namespace Lab2.Elements;

public class SubProcess
{
    private int _id;
    public readonly string Name;
    public bool IsWorking { get; private set; }
    public double NextT { get; private set; } = double.MaxValue;
    
    public int Quantity { get; private set; }
    public double WorkTime { get; private set; }
    
    public SubProcess(int processId, int subProcessId)
    {
        _id = subProcessId;
        Name = $"subProcess_{processId}.{subProcessId}";
    }

    public void InAct(double nextT)
    {
        IsWorking = true;
        NextT = nextT;
    }
    
    public void DoStatistics(double delta)
    {
        WorkTime += IsWorking? delta : 0;
    }

    public void OutAct()
    {
        IsWorking = false;
        NextT = double.MaxValue;
        Quantity++;
    }
}