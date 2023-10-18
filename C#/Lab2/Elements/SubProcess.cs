namespace Lab2.Elements;

public class SubProcess
{
    private int _id;
    public string Name;
    
    public int State { get; set; }
    public double nextT { get; set; } = double.MaxValue;
    
    public int Quantity { get; private set; }
    public double WorkTime { get; private set; }
    
    public SubProcess(int processId, int subProcessId)
    {
        _id = subProcessId;
        Name = $"subProcess_{processId}.{subProcessId}";
    }
    
    public void DoStatistics(double delta)
    {
        WorkTime += delta * State;
    }
    
}