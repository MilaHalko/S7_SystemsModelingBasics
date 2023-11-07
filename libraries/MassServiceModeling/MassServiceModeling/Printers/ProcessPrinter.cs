using MassServiceModeling.Elements;
using MassServiceModeling.Statistics;

namespace MassServiceModeling.Printers;

public class ProcessPrinter : IPrinter
{
    private Process p;
    private ProcessStatisticHelper s => p.StatisticHelper;

    public ProcessPrinter(Process process)
    {
        p = process;
    }

    public void Info()
    {
        Console.Write($"{IPrinter.PrintState(p.IsWorking)}{p.Name}  tnext={IPrinter.Format(p.Time.Next)}");
        if (p.Queue.Length > 0) Console.Write($"  queue={p.Queue.Length}");
        if (s.Failure > 0) Console.Write($"  failure={s.Failure}");
        if (p.SubProcesses.Count >= 10 && p.SubProcesses.WorkingCount > 0) Console.WriteLine($"  workingSubProcesses count = {p.SubProcesses.WorkingCount}");
        Console.WriteLine();
        
        if (p.SubProcesses.Count is > 1 and < 10)
        {
            foreach (var subProcess in p.SubProcesses.All)
                subProcess.Printer.Info();
        }
    }

    public void Statistics()
    {
        Console.WriteLine($"{p.Name}:");
        Console.WriteLine($"\tWorkTime = {s.WorkTime / p.Time.Curr}");
        Console.WriteLine($"\tInActQuantity = {s.InActQuantity}");
        Console.WriteLine($"\tOutActQuantity = {s.OutActQuantity}");
        Console.WriteLine($"\tCurrent queue length = {p.Queue.Length}");
        Console.WriteLine($"\tMean length of queue = {s.MeanQueueAllTime / p.Time.Curr}");
        Console.WriteLine($"\tFailure probability = {s.Failure / (double)s.InActQuantity}");

        if (p.SubProcesses.Count >= 10)
            Console.WriteLine($"\tSubProcesses Total: count={p.SubProcesses.Count} quantity={p.SubProcesses.All.Sum(sp => sp.StatisticHelper.Quantity)}");
        else
            foreach (var subProcess in p.SubProcesses.All)
                subProcess.Printer.Statistics();
    }
}