using MassServiceModeling.Elements;
using MassServiceModeling.Statistics;

namespace MassServiceModeling.Printers;

public class CreatePrinter : IPrinter
{
    private Create _c;
    private ElementStatisticHelper _bs => _c.BaseStatistic;

    public CreatePrinter(Create create) => _c = create;

    public void Info()
    {
        Console.Write($"  {_c.Name} created={_bs.OutActQuantity}  " +
                      $"delay={IPrinter.Format(_c.Delay)}  " +
                      $"tnext={IPrinter.Format(_c.NextT)  }");
        Console.WriteLine();
    }

    public void Statistics()
    {
        Console.WriteLine($"{_c.Name}:");
        Console.WriteLine($"\tQuantity = {_bs.OutActQuantity}");
    }
}