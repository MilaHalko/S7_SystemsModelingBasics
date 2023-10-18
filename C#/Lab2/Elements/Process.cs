namespace Lab2.Elements;

public class Process : Element
{
    private List<SubProcess> _subProcesses;
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
        base.InAct();
        if (State < _subProcesses.Count)
        {
            State++;
            NextT = CurrT + GetDelay();
        }
        else
        {
            if (_queue < _maxQueue)
                _queue++;
            else
                Failure++;
        }
    }

    public override void OutAct()
    {
        base.OutAct();
        NextT = double.MaxValue;
        State--;
        if (_queue > 0)
        {
            _queue--;
            State++;
            NextT = CurrT + GetDelay();
        }
    }

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