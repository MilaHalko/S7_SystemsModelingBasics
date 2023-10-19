namespace Lab2.Elements;

public class SubProcess
{
    private int _id;
    public string Name;
    
    // TODO:Set to IsFree or IsBusy
    public int State { get; set; }
    public double NextT { get; set; } = double.MaxValue;
    
    public int Quantity { get; private set; }
    public double WorkTime { get; private set; }
    
    public SubProcess(int processId, int subProcessId)
    {
        _id = subProcessId;
        Name = $"subProcess_{processId}.{subProcessId}";
    }

    public void InAct(double nextT)
    {
        State = 1;
        NextT = nextT;
    }

    
    public void DoStatistics(double delta)
    {
        WorkTime += delta * State;
    }

    public void OutAct()
    {
        State = 0;
        NextT = double.MaxValue;
        Quantity++;
    }
}