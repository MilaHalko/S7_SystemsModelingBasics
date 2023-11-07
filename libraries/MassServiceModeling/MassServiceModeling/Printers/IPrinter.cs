using MassServiceModeling.Elements;
using MassServiceModeling.Items;

namespace MassServiceModeling.Printers;

public interface IPrinter
{
    public static Item? CurrentItem { get; set; }
    protected static string GetItemName(Item item) => item.Name == "" ? $"Item_{item.Id}" : $"{item.Name}_{item.Id}";
    public void Statistics();
    public void Info();

    public static string Format(double num) => num == double.MaxValue ? "\u221E" : num.ToString("F5");

    public static void Info(List<Element> elements, Element current)
    {
        foreach (var element in elements)
        {
            if (element == current)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                element.Print.Info();
                Console.ResetColor();
            }
            else element.Print.Info();
        }
    }

    public static void Result(List<Element> elements) 
    {
        Console.WriteLine("\n-------------RESULTS-------------");
        foreach (var element in elements)
            element.Print.Statistics();
    }

    public static void PrintCurrent(Element element)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.ResetColor();
        Console.Write($"Event: {element.Name}  " +
                      $"StartTime: {Format(element.Time.Curr)}  " +
                      $"Delay: {Format(element.Time.Delay)}  " +
                      $"CurrentTime: {Format(element.Time.Next)}  " +
                      $"Item: {CurrentItem!.Name}_{CurrentItem.Id}  ");
        Console.WriteLine();
    }
    
    public static string PrintState(bool state) => state ? "\u25ba " : "  ";
}