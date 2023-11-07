using MassServiceModeling.Elements;
using MassServiceModeling.Statistics;

namespace MassServiceModeling.Printers;

public class ElementPrinter : IPrinter
{
    private Element e;
    private ElementStatisticHelper s => e.StatisticHelper;

    public ElementPrinter(Element element) => e = element;

    public void Info()
    {
        Console.WriteLine($"{e.Name} state={IPrinter.PrintState(e.IsWorking)} quantity={s.InActQuantity} tnext={IPrinter.Format(e.Time.Next)}\n");
    }

    public void Statistics()
    {
        Console.Out.WriteLine($"{e.Name}:");
        Console.WriteLine($"\tQuantity = {s.InActQuantity}");
        Console.WriteLine($"\tQuantity processed = {s.OutActQuantity}");
        Console.WriteLine($"\tWorkTime = {s.WorkTime}");
    }
}