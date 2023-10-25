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
        Console.WriteLine($"{e.Name} state = {e.IsWorking} quantity = {e.Quantity} tnext= {IPrinter.Format(e.NextT)}\n");
    }

    public void Statistics()
    {
        Console.Out.WriteLine($"{e.Name}:");
        Console.WriteLine($"\tQuantity = {e.Quantity}");
        Console.WriteLine($"\tQuantity processed = {e.QuantityProcessed}");
        Console.WriteLine($"\tWorkTime = {e.WorkTime}");
    }
}