using MassServiceModeling.Enums;
using MassServiceModeling.Printers;

namespace MassServiceModeling.Elements;

public class Process : Element
{
    public int Failure { get; private set; }
    public double MeanQueue { get; private set; }
    public int Queue { get; private set; }
    public List<SubProcess> SubProcesses { get; } = new();
    private readonly int _maxQueue;
    
    private int WorkingSubProcessesCount => SubProcesses.Count(s => s.IsWorking);
    private SubProcess FreeSubProcess => SubProcesses.First(s => !s.IsWorking);
    private List<SubProcess> BusySubProcesses => SubProcesses.Where(s => s.NextT <= CurrT && s.IsWorking).ToList();
    
    public Process(int subProcessCount, double delay = 1.0, Distribution distribution = Distribution.Exponential, string name = "PROCESS",
        int maxQueue = int.MaxValue) :
        base(delay, name, distribution)
    {
        for (int i = 0; i < subProcessCount; i++)
            SubProcesses.Add(new SubProcess(Id, i));
        _maxQueue = maxQueue;
        NextT = double.MaxValue;
        Print = new ProcessPrinter(this);
    }

    public override void InAct()
    {
        base.InAct();
        if (WorkingSubProcessesCount < SubProcesses.Count)
            FreeSubProcess.InAct(CurrT + GetDelay());
        else
        {
            if (Queue < _maxQueue) Queue++;
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
            if (Queue > 0)
            {
                Queue--;
                subProcess.InAct(CurrT + GetDelay());
            }
        }
        UpdateNextT();
    }

    public override void DoStatistics(double delta)
    {
        base.DoStatistics(delta);
        foreach (var subProcess in SubProcesses) subProcess.DoStatistics(delta);
        MeanQueue += Queue * delta;
    }

    protected override void UpdateNextT() => NextT = SubProcesses.Min(s => s.NextT);
}