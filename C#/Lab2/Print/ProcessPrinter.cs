using Lab2.Elements;

namespace Lab2.Print;

public class ProcessPrinter : IPrinter
{
    private Process p;

    public ProcessPrinter(Process process)
    {
        p = process;
    }

    public void Info()
    {
        Console.WriteLine($"{p.Name} state = {p.IsWorking} quantity = {p.Quantity} tnext= {IPrinter.Format(p.NextT)} queue = {p.Queue} failure = {p.Failure}");
        foreach (var subProcess in p.SubProcesses)
            subProcess.Printer.Info();
        Console.WriteLine();
    }

    public void Statistics()
    {
        Console.WriteLine($"{p.Name}:");
        Console.WriteLine($"\tQuantity = {p.Quantity}");
        Console.WriteLine($"\tWorkTime = {p.WorkTime / p.CurrT}");
        Console.WriteLine($"\tMean length of queue = {p.MeanQueue / p.CurrT}");
        Console.WriteLine($"\tFailure probability = {p.Failure / (double)p.Quantity}");
        // Console.WriteLine($"\tFinal queue = {p.Queue}");
        foreach (var subProcess in p.SubProcesses)
            subProcess.Printer.Statistics();
    }
}