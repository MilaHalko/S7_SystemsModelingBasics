using MassServiceModeling.Elements;
using MassServiceModeling.Items;
using MassServiceModeling.Printers;
using MassServiceModeling.Statistics;
using MassServiceModeling.Time;

namespace MassServiceModeling.SubProcesses;

public class SubProcess
{
    // Dynamic attributes
    public Item? Item { private set; get; }
    public TimeHelper Time = new();
    public bool IsWorking { get; private set; }

    // Static attributes
    public string Name { get; }
    public Process Process { get; }
    public SubProcessStatisticHelper StatisticHelper;
    public SubProcessPrinter Printer { get; private init; }
    
    public SubProcess(Process process, int subProcessId, string name)
    {
        Process = process;
        Name = name == "" ? "SubProcess" : name;
        Name = $"{Name}_{subProcessId}";
        StatisticHelper = new SubProcessStatisticHelper(this);
        Printer = new SubProcessPrinter(this);
    }

    public void InAct(double nextT, Item item)
    {
        IsWorking = true;
        StatisticHelper.Quantity++;
        Time.Next = nextT;
        Time.Delay = nextT - Process.Time.Curr;
        Item = item;
    }

    public Item OutAct()
    {
        IsWorking = false;
        Time.Next = double.MaxValue;
        return Item!;
    }

    public void DoStatistics(double delta)
    {
        StatisticHelper.WorkTime += IsWorking? delta : 0;
    }
}