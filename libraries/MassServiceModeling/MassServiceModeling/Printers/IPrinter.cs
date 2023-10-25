using System.Globalization;
using MassServiceModeling.Elements;

namespace MassServiceModeling.Printers;

public interface IPrinter
{
    public void Statistics();
    public void Info();

    public static string Format(double num) => num == double.MaxValue ? "\u221E" : num.ToString(CultureInfo.InvariantCulture);

    public static void Info(List<Element> elements)
    {
        foreach (var element in elements)
            element.Print.Info();
    }

    public static void Result(List<Element> elements)
    {
        Console.WriteLine("\n-------------RESULTS-------------");
        foreach (var element in elements)
            element.Print.Statistics();
    }

    public static void PrintCurrent(Element element)
    {
        Console.WriteLine($"\nNext event: {element.Name}  Time: {element.NextT} ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    }
}