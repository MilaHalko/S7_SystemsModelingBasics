using MassServiceModeling.Elements;
using MassServiceModeling.Statistics;

namespace MassServiceModeling.Printers;

public class ElementPrinter : IPrinter
{
    private Element _e;
    private ElementStatisticHelper _bs => _e.BaseStatistic;

    public ElementPrinter(Element element) => _e = element;

    public void Info()
    {
        Console.WriteLine($"{_e.Name} state={IPrinter.PrintState(_e.IsWorking)} quantity={_bs.InActQuantity} tnext={IPrinter.Format(_e.NextT)}\n");
    }

    public void Statistics()
    {
        Console.Out.WriteLine($"{_e.Name}:");
        Console.WriteLine($"\tQuantity = {_bs.InActQuantity}");
        Console.WriteLine($"\tQuantity processed = {_bs.OutActQuantity}");
        Console.WriteLine($"\tWorkTime = {_bs.WorkTime}");
    }
}