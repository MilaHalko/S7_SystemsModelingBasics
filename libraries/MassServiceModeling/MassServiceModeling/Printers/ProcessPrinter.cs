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
        Console.WriteLine(
            $"{p.Name}{IPrinter.PrintState(p.IsWorking)} queue={p.Queue} next={IPrinter.Format(p.NextT)}");
            // $"quantity={p.Quantity} ");
            // $"failure={p.Failure}");
        if (p.SubProcesses.Count > 1)
            foreach (var subProcess in p.SubProcesses)
                subProcess.Printer.Info();
    }

    public void Statistics()
    {
        Console.WriteLine($"{p.Name}:");
        Console.WriteLine($"\tQuantity = {p.Quantity}");
        Console.WriteLine($"\tWorkTime = {p.WorkTime / p.CurrT}");
        Console.WriteLine($"\tMean length of queue = {p.MeanQueue / p.CurrT}");
        Console.WriteLine($"\tFailure probability = {p.Failure / (double)p.Quantity}");
        // Console.WriteLine($"\tFinal queue = {p.Queue}");
       
        if (p.SubProcesses.Count > 1)
            foreach (var subProcess in p.SubProcesses)
                subProcess.Printer.Statistics();
    }
}