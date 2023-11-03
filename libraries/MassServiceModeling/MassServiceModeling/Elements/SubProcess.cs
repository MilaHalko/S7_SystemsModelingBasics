using MassServiceModeling.Printers;

namespace MassServiceModeling.Elements;

public class SubProcess
{
    // Statistics
    public int Quantity { get; private set; }
    public double WorkTime { get; private set; }

    // Dynamic attributes
    public Item? Item { private set; get; }
    public double NextT { get; private set; } = double.MaxValue;
    public double Delay { get; set; }
    public bool IsWorking { get; private set; }

    // Static attributes
    public string Name { get; }
    public SubProcessPrinter Printer { get; private init; }
    protected Process Process { get; }


    public SubProcess(Process process, int subProcessId, string name)
    {
        Process = process;
        Name = name == "" ? "SubProcess" : name;
        Name = $"{Name}_{subProcessId}";
        Printer = new SubProcessPrinter(this);
    }

    public void InAct(double nextT, Item item)
    {
        IsWorking = true;
        Quantity++;
        NextT = nextT;
        Delay = nextT - Process.CurrT;
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
        WorkTime += IsWorking? delta : 0;
    }
}