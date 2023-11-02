using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Printers;

namespace MassServiceModeling.Elements;

public class Process : Element
{
    public int Failure { get; private set; }
    public double MeanQueueAllTime { get; private set; }
    public int Queue { get; set; }
    public double TotalTimeBetweenOutActs { get; private set; }
    public List<SubProcess> SubProcesses { get; } = new();

    public event Action? OnQueueChanged;

    private readonly int _maxQueue;
    private double? _lastOutActTime;
    private int WorkingSubProcessesCount => SubProcesses.Count(s => s.IsWorking);
    private SubProcess FreeSubProcess => SubProcesses.First(s => !s.IsWorking);
    private List<SubProcess> BusySubProcesses => SubProcesses.Where(s => s.NextT <= CurrT && s.IsWorking).ToList();

    public Process(Randomizer randomizer, int subProcessCount = 1, string name = "PROCESS", int maxQueue = int.MaxValue) :
        base(randomizer, name)
    {
        for (int i = 0; i < subProcessCount; i++)
            SubProcesses.Add(new SubProcess(Id, i));
        _maxQueue = maxQueue;
        NextT = double.MaxValue;
        Print = new ProcessPrinter(this);
    }

    public Process(double delay = 1.0, int subProcessCount = 1, string name = "PROCESS", int maxQueue = int.MaxValue) :
        this(new ExponentialRandomizer(delay), subProcessCount, name, maxQueue) {}

    public override void InAct()
    {
        base.InAct();
        if (WorkingSubProcessesCount < SubProcesses.Count)
            FreeSubProcess.InAct(CurrT + GetDelay());
        else
        {
            if (Queue < _maxQueue)
            {
                Queue++;
                OnQueueChanged?.Invoke();
            }
            else Failure++;
        }

        UpdateNextT();
    }

    public override void OutAct()
    {
        foreach (var subProcess in BusySubProcesses)
        {
            subProcess.OutAct();
            base.OutAct();
            TotalTimeBetweenOutActs += CurrT - _lastOutActTime ?? 0;
            _lastOutActTime = CurrT;
            if (Queue > 0)
            {
                IsWorking = true;
                Queue--;
                OnQueueChanged?.Invoke();
                subProcess.InAct(CurrT + GetDelay());
            }
        }

        UpdateNextT();
    }

    public override void DoStatistics(double delta)
    {
        base.DoStatistics(delta);
        foreach (var subProcess in SubProcesses) subProcess.DoStatistics(delta);
        MeanQueueAllTime += Queue * delta;
    }

    protected override void UpdateNextT() => NextT = SubProcesses.Min(s => s.NextT);
}