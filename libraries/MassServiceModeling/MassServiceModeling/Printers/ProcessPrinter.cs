using MassServiceModeling.Elements;
using MassServiceModeling.Statistics;
using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.Printers;

public class ProcessPrinter : IPrinter
{
    private Process _p;
    private ElementStatisticHelper _bs => _p.BaseStatistic;
    private ProcessStatisticHelper _ps => _p.ProcessStatistic;

    public ProcessPrinter(Process process) => _p = process;

    public void Info()
    {
        Console.Write($"{IPrinter.PrintState(_p.IsWorking)}{_p.Name}  tnext={IPrinter.Format(_p.NextT)}");
        if (_p.Queue.Length > 0) Console.Write($"  queue={_p.Queue.Length}");
        if (_ps.Failure > 0) Console.Write($"  failure={_ps.Failure}");
        if (_p.SubProcesses is { Count: >= 10, WorkingCount: > 0 }) Console.WriteLine($"  workingSubProcesses count = {_p.SubProcesses.WorkingCount}");
        Console.WriteLine();

        if (_p.SubProcesses.Count is <= 1 or >= 10) return;
        foreach (var subProcess in _p.SubProcesses.All)
            subProcess.Printer.Info();
    }

    public void Statistics()
    {
        Console.WriteLine($"{_p.Name}:");
        Console.WriteLine($"\tWorkTime = {_bs.WorkTime / Time.Curr}");
        Console.WriteLine($"\tInActQuantity = {_bs.InActQuantity}");
        Console.WriteLine($"\tOutActQuantity = {_bs.OutActQuantity}");
        Console.WriteLine($"\tCurrent queue length = {_p.Queue.Length}");
        Console.WriteLine($"\tMean length of queue = {_ps.MeanQueueAllTime / Time.Curr}");
        Console.WriteLine($"\tFailure probability = {_ps.Failure / (double)_bs.InActQuantity}");

        if (_p.SubProcesses.Count >= 10)
            Console.WriteLine($"\tSubProcesses Total: count={_p.SubProcesses.Count} quantity={_p.SubProcesses.All.Sum(sp => sp.StatisticHelper.Quantity)}");
        else
            foreach (var subProcess in _p.SubProcesses.All)
                subProcess.Printer.Statistics();
    }
}