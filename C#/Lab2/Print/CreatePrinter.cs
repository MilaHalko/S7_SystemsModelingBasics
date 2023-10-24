using Lab2.Elements;

namespace Lab2.Print;

public class CreatePrinter : IPrinter
{
    private Create c;

    public CreatePrinter(Create create)
    {
        c = create;
    }

    public void Info()
    {
        Console.WriteLine($"{c.Name} quantity = {c.QuantityProcessed} tnext= {IPrinter.Format(c.NextT)}\n");
    }
    
    public void Statistics()
    {
        Console.WriteLine($"{c.Name}:");
        Console.WriteLine($"\tQuantity = {c.QuantityProcessed}");
    }
}