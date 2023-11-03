using MassServiceModeling.Elements;

namespace MassServiceModeling.Printers;

public class CreatePrinter : IPrinter
{
    private Create c;

    public CreatePrinter(Create create)
    {
        c = create;
    }

    public void Info()
    {
        Console.Write($"  {c.Name} created={c.OutActQuantity}  " +
                      $"delay={IPrinter.Format(c.Delay)}  " +
                      $"tnext={IPrinter.Format(c.NextT)  }");
        Console.WriteLine();
    }

    public void Statistics()
    {
        Console.WriteLine($"{c.Name}:");
        Console.WriteLine($"\tQuantity = {c.OutActQuantity}");
    }
}