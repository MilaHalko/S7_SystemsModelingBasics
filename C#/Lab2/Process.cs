namespace Lab2;

public class Process : Element
{
    private int _queue;
    private readonly int _maxQueue;
    public int Failure {get; private set;}
    public double MeanQueue {get; private set; }

    public Process(double delay = 1.0, string name = "",  string distribution = "exp", int maxQueue = int.MaxValue) : base(delay, name, distribution)
    {
        _maxQueue = maxQueue;
    }

    public override void InAct()
    {
        if (State == 0)
        {
            State = 1;
            Tnext = Tcurr + GetDelay();
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
        Tnext = double.MaxValue;
        State = 0;
        if (_queue > 0)
        {
            _queue--;
            State = 1;
            Tnext = Tcurr + GetDelay();
        }
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"failure = {this.Failure}");
    }

    public override void DoStatistics(double delta)
    {
        MeanQueue += _queue * delta;
    }
}