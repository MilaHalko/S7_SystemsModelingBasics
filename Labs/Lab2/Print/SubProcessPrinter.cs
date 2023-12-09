using Lab2.Elements;

namespace Lab2.Print;

public class SubProcessPrinter : IPrinter
{
    private SubProcess s;

    public SubProcessPrinter(SubProcess subProcess)
    {
        s = subProcess;
    }

    public void Statistics()
    {
        Console.Out.WriteLine($"\t{s.Name}:");
        Console.WriteLine($"\t\tQuantity = {s.Quantity}");
    }

    public void Info()
    {
        Console.WriteLine(
            $"\t{s.Name} state = {s.IsWorking} quantity = {s.Quantity} tnext= {IPrinter.Format(s.NextT)}");
    }
}