using MassServiceModeling.Items;
using MassServiceModeling.Printers;
using MassServiceModeling.Statistics;
using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.SubProcesses;

public class SubProcess
{
    // Dynamic attributes
    public Item? Item { private set; get; }
    public bool IsWorking { get; private set; }

    // Static attributes
    public string Name { get; }
    public Time Time = new();
    public Time ProcessTime { get; }
    public SubProcessStatisticHelper StatisticHelper;
    public SubProcessPrinter Printer { get; private init; }
    
    public SubProcess(Time processTime, int subProcessId, string name)
    {
        ProcessTime = processTime;
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
        Time.Delay = nextT - ProcessTime.Curr;
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