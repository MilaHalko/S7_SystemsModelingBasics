using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Items;
using MassServiceModeling.Printers;
using MassServiceModeling.Statistics;
using MassServiceModeling.SubProcesses;
using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.Elements;

public class Process : Element
{
    public event Action? OnQueueChanged;
    public ItemsQueue Queue;
    public SubProcessesContainer SubProcesses = new();
    public ProcessStatisticHelper ProcessStatistic = new();

    public Process(Randomizer randomizer, int subProcessCount = 1, string name = "", int maxQueue = int.MaxValue, String subProcessName = "") 
        : base(randomizer, name)
    {
        for (var i = 0; i < subProcessCount; i++) SubProcesses.Add(new SubProcess(i, subProcessName));
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
        foreach (var subProcess in SubProcesses.ForOutAct)
        {
            Item = subProcess.OutAct();
            
            var nextElements = NextElementsContainer;
            NextElementsContainerSetup();
            base.OutAct();
            NextElementsContainer = nextElements;
            
            if (SubProcesses.WorkingCount > 0) IsWorking = true;
            if (Queue.Length > 0)
            {
                IsWorking = true;
                Item = Queue.GetItem();
                OnQueueChanged?.Invoke();
                subProcess.InAct(Time.Curr + GetDelay(), Item);
            }
        }
        UpdateNextT();
    }

    public override void DoStatistics(double delta)
    {
        base.DoStatistics(delta);
        foreach (var subProcess in SubProcesses.All) subProcess.DoStatistics(delta);
        ProcessStatistic.MeanQueueAllTime += Queue.Length * delta;
    }

    public static void TryChangeQueueForLastItem(Process from, Process to)
    {
        if (!ItemsQueue.TrySwapLast(from.Queue, to.Queue)) return;
        from.OnQueueChanged?.Invoke();
        to.OnQueueChanged?.Invoke();
    }

    protected override void SetItem(Item item)
    {
        if (SubProcesses.WorkingCount < SubProcesses.Count)
        {
            Item = item;
            SubProcesses.Free.InAct(Time.Curr + GetDelay(), item);
        }
        else
        {
            if (Queue.TryAdd(item)) OnQueueChanged?.Invoke();
            else ProcessStatistic.Failure++;
        }
    }
    
    protected override void UpdateNextT() => NextT = SubProcesses.All.Min(s => s.NextT);
    protected override string GetElementDefaultName() => "PROCESS";
    protected virtual void NextElementsContainerSetup() {}
}