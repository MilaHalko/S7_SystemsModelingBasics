using MassServiceModeling.Items;
using MassServiceModeling.Printers;
using MassServiceModeling.Statistics;
using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.SubProcesses;

public class SubProcess
{
    // Dynamic attributes
    public double NextT { get; private set; } = double.MaxValue;
    public Item? Item { private set; get; }
    public double Delay { get; private set; }
    public bool IsWorking { get; private set; }

    // Static attributes
    public string Name { get; }
    public SubProcessStatisticHelper StatisticHelper;
    public SubProcessPrinter Printer { get; private init; }
    
    public SubProcess(int subProcessId, string name)
    {
        Name = name == "" ? "SubProcess" : name;
        Name = $"{Name}_{subProcessId}";
        StatisticHelper = new SubProcessStatisticHelper();
        Printer = new SubProcessPrinter(this);
    }

    public void InAct(double nextT, Item item)
    {
        IsWorking = true;
        StatisticHelper.Quantity++;
        NextT = nextT;
        Delay = nextT - Time.Curr;
        Item = item;
    }

    public Item OutAct()
    {
        IsWorking = false;
        NextT = double.MaxValue;
        return Item!;
    }

    public void DoStatistics(double delta)
    {
        StatisticHelper.WorkTime += IsWorking? delta : 0;
    }
}