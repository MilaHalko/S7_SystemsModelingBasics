using MassServiceModeling.Elements;

namespace MassServiceModeling.Printers;

public class ProcessPrinter : IPrinter
{
    private Process p;

    public ProcessPrinter(Process process)
    {
        p = process;
    }

    public void Info()
    {
        Console.Write($"{IPrinter.PrintState(p.IsWorking)}{p.Name}  tnext={IPrinter.Format(p.NextT)}");
        if (p.QueueLength > 0) Console.Write($"  queue={p.QueueLength}");
        if (p.Failure > 0) Console.Write($"  failure={p.Failure}");
        if (p.SubProcesses.Count is > 1 and < 10)
        {
            Console.WriteLine();
            foreach (var subProcess in p.SubProcesses)
                subProcess.Printer.Info();
        }
        else if (p.SubProcesses.Count >= 10 && p.WorkingSubProcessesCount > 0)
            Console.WriteLine($"  workingSubProcesses count = {p.WorkingSubProcessesCount}");
        
        Console.WriteLine();
    }

    public void Statistics()
    {
        Console.WriteLine($"{p.Name}:");
        Console.WriteLine($"\tQuantity = {p.InActQuantity}");
        Console.WriteLine($"\tWorkTime = {p.WorkTime / p.CurrT}");
        Console.WriteLine($"\tMean length of queue = {p.MeanQueueAllTime / p.CurrT}");
        Console.WriteLine($"\tFailure probability = {p.Failure / (double)p.InActQuantity}");

        if (p.SubProcesses.Count >= 10)
            Console.WriteLine($"\tSubProcesses Total: count={p.SubProcesses.Count} quantity={p.SubProcesses.Sum(s => s.Quantity)}");
        else
            foreach (var subProcess in p.SubProcesses)
                subProcess.Printer.Statistics();
    }
}