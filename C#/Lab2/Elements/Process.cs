namespace Lab2.Elements;

public class Process : Element
{
    private List<SubProcess> _subProcesses = new List<SubProcess>();
    private int _queue;
    private readonly int _maxQueue;
    public int Failure { get; private set; }
    public double MeanQueue { get; private set; }

    public Process(int subProcessCount, double delay = 1.0, string distribution = "exp", string name = "PROCESS",
        int maxQueue = int.MaxValue) :
        base(delay, name, distribution)
    {
        for (int i = 0; i < subProcessCount; i++)
            _subProcesses.Add(new SubProcess(_id, i));
        _maxQueue = maxQueue;
        NextT = double.MaxValue;
    }

    public override void InAct()
    {
        if (State < _subProcesses.Count)
        {
            State++;
            GetFreeSubProcess().InAct(CurrT + GetDelay());
        }
        else
        {
            if (_queue < _maxQueue) _queue++;
            else Failure++;
        }
        UpdateNextT();
    }

    private void UpdateNextT() => NextT = _subProcesses.Min(s => s.NextT);

    private SubProcess GetFreeSubProcess() => _subProcesses.First(s => s.State == 0);

    public override void OutAct()
    {
        var busySubProcesses = GetBusySubProcesses();
        foreach (var subProcess in busySubProcesses)
        {
            subProcess.OutAct();
            State--;
            base.OutAct();
            if (_queue > 0)
            {
                _queue--;
                State++;
                subProcess.InAct(CurrT + GetDelay());
            }
        }
        UpdateNextT();
    }

    private List<SubProcess> GetBusySubProcesses() =>
        _subProcesses.Where(s => s.NextT <= CurrT && s.State == 1).ToList();

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"failure = {this.Failure}");
    }

    public override void DoStatistics(double delta)
    {
        base.DoStatistics(delta);
        MeanQueue += _queue * delta;
    }
}