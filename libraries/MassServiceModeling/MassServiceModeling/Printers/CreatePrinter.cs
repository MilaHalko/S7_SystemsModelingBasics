using MassServiceModeling.Elements;
using MassServiceModeling.Statistics;

namespace MassServiceModeling.Printers;

public class CreatePrinter : IPrinter
{
    private Create c;
    private CreateStatisticHelper s => c.StatisticHelper;

    public CreatePrinter(Create create) => c = create;

    public void Info()
    {
        Console.Write($"  {c.Name} created={s.OutActQuantity}  " +
                      $"delay={IPrinter.Format(c.Time.Delay)}  " +
                      $"tnext={IPrinter.Format(c.Time.Next)  }");
        Console.WriteLine();
    }

    public void Statistics()
    {
        Console.WriteLine($"{c.Name}:");
        Console.WriteLine($"\tQuantity = {s.OutActQuantity}");
    }
}