using Lab2.Elements;

namespace Lab2.Print;

public class ElementPrinter : IPrinter
{
    private Element e;

    public ElementPrinter(Element element)
    {
        e = element;
    }

    public void Statistics()
    {
        Console.Out.WriteLine($"\t{e.Name}:");
        Console.WriteLine($"\t\tQuantity = {e.Quantity}");
        Console.WriteLine($"\t\tWorkTime = {e.WorkTime}");
    }

    public void Info()
    {
        Console.WriteLine($"\t{e.Name} state = {e.IsWorking} quantity = {e.Quantity} tnext= {IPrinter.Format(e.NextT)}");
    }
}