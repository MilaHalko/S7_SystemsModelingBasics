using Lab2.Print;

namespace Lab2.Elements;

public class SubProcess
{
    public readonly string Name;
    public bool IsWorking { get; private set; }
    public double NextT { get; private set; } = double.MaxValue;
    
    public int Quantity { get; private set; }
    public double WorkTime { get; private set; }
    public SubProcessPrinter Printer { get; private init; }
    
    public SubProcess(int processId, int subProcessId)
    {
        Name = $"subProcess_{processId}.{subProcessId}";
        Printer = new SubProcessPrinter(this);
    }

    public void InAct(double nextT)
    {
        IsWorking = true;
        Quantity++;
        NextT = nextT;
    }

    public void OutAct()
    {
        IsWorking = false;
        NextT = double.MaxValue;
    }

    public void DoStatistics(double delta)
    {
        WorkTime += IsWorking? delta : 0;
    }
}