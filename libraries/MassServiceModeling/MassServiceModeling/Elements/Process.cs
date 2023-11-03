using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Printers;
using MassServiceModeling.ItemsQueues;
namespace MassServiceModeling.Elements;

public class Process : Element
{
    // Statistics
    public int Failure { get; private set; }
    public double MeanQueueAllTime { get; private set; }
    
    // SubProcesses
    public List<SubProcess> SubProcesses { get; } = new();
    public int WorkingSubProcessesCount => SubProcesses.Count(s => s.IsWorking);
    protected List<SubProcess> SubProcessesForOutAct => SubProcesses.Where(s => s.NextT <= CurrT && s.IsWorking).ToList();
    private SubProcess FreeSubProcess => SubProcesses.First(s => !s.IsWorking);

    // Queue
    public event Action? OnQueueChanged;
    public int QueueLength => Queue.Length;
    protected ItemsQueue Queue;

    public Process(Randomizer randomizer, int subProcessCount = 1, string name = "", int maxQueue = int.MaxValue, String subProcessName = "") 
        : base(randomizer, name)
    {
        for (int i = 0; i < subProcessCount; i++)
            SubProcesses.Add(new SubProcess(this, i, subProcessName));
        Queue = new ItemsQueue(maxQueue);
        NextT = double.MaxValue;
        Print = new ProcessPrinter(this);
    }

    public Process(double delay = 1.0, int subProcessCount = 1, string name = "", int maxQueue = int.MaxValue, String subProcessName = "") 
        : this(new ExponentialRandomizer(delay), subProcessCount, name, maxQueue, subProcessName) {}

    public override void InAct(Item item)
    {
        base.InAct(item);
        UpdateNextT();
    }

    public override void OutAct()
    {
        foreach (var subProcess in SubProcessesForOutAct)
        {
            Item = subProcess.OutAct();
            
            var nextElements = NextElementsContainer;
            NextElementsContainerSetup();
            base.OutAct();
            NextElementsContainer = nextElements;
            
            if (WorkingSubProcessesCount > 0) IsWorking = true;
            if (QueueLength > 0)
            {
                IsWorking = true;
                Item = Queue.GetItem();
                OnQueueChanged?.Invoke();
                subProcess.InAct(CurrT + GetDelay(), Item);
            }
        }

        UpdateNextT();
    }

    public override void DoStatistics(double delta)
    {
        base.DoStatistics(delta);
        foreach (var subProcess in SubProcesses) subProcess.DoStatistics(delta);
        MeanQueueAllTime += QueueLength * delta;
    }

    public static void TryChangeQueueForLastItem(Process from, Process to)
    {
        if (ItemsQueue.TrySwapLast(from.Queue, to.Queue))
        {
            from.OnQueueChanged?.Invoke();
            to.OnQueueChanged?.Invoke();
        }
    }

    protected override void SetItem(Item item)
    {
        if (WorkingSubProcessesCount < SubProcesses.Count)
        {
            Item = item;
            FreeSubProcess.InAct(CurrT + GetDelay(), item);
        }
        else
        {
            if (Queue.TryAdd(item)) OnQueueChanged?.Invoke();
            else Failure++;
        }
    }
    
    protected override void UpdateNextT() => NextT = SubProcesses.Min(s => s.NextT);

    protected override string GetElementName() => "PROCESS";

    protected virtual void NextElementsContainerSetup() {}
}