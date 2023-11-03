using MassServiceModeling.Elements;

namespace MassServiceModeling.Printers;

public class ElementPrinter : IPrinter
{
    private Element e;

    public ElementPrinter(Element element)
    {
        e = element;
    }

    public void Info()
    {
        Console.WriteLine($"{e.Name} state={IPrinter.PrintState(e.IsWorking)} quantity={e.InActQuantity} tnext={IPrinter.Format(e.NextT)}\n");
    }

    public void Statistics()
    {
        Console.Out.WriteLine($"{e.Name}:");
        Console.WriteLine($"\tQuantity = {e.InActQuantity}");
        Console.WriteLine($"\tQuantity processed = {e.OutActQuantity}");
        Console.WriteLine($"\tWorkTime = {e.WorkTime}");
    }
}