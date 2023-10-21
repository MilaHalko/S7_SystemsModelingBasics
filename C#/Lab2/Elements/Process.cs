namespace Lab2.Elements;

public class Process : Element
{
    private List<SubProcess> _subProcesses = new();
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

    public int WorkingSubProcessesCount => _subProcesses.Count(s => s.IsWorking);

    private SubProcess GetFreeSubProcess() => _subProcesses.First(s => !s.IsWorking);

    private List<SubProcess> GetBusySubProcesses() =>
        _subProcesses.Where(s => s.NextT <= CurrT && s.IsWorking).ToList();

    private void UpdateNextT() => NextT = _subProcesses.Min(s => s.NextT);

    public override void InAct()
    {
        base.InAct();
        if (WorkingSubProcessesCount < _subProcesses.Count)
            GetFreeSubProcess().InAct(CurrT + GetDelay());
        else
        {
            if (_queue < _maxQueue) _queue++;
            else Failure++;
        }

        UpdateNextT();
    }

    public override void OutAct()
    {
        var busySubProcesses = GetBusySubProcesses();
        foreach (var subProcess in busySubProcesses)
        {
            subProcess.OutAct();
            base.OutAct();
            if (_queue > 0)
            {
                _queue--;
                subProcess.InAct(CurrT + GetDelay());
            }
        }

        UpdateNextT();
    }

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"{Name} state = {IsWorking} quantity = {Quantity} tnext= {NextTString(NextT)} queue = {_queue}");
        foreach (var subProcess in _subProcesses)
            Console.WriteLine($"\t{subProcess.Name} state = {subProcess.IsWorking} tnext = {NextTString(subProcess.NextT)}");
        Console.WriteLine($"failure = {this.Failure}");
    }


    public override void DoStatistics(double delta)
    {
        base.DoStatistics(delta);
        MeanQueue += _queue * delta;
    }
}