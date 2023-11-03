using MassServiceModeling.Elements;

namespace MassServiceModeling.Printers;

public class SubProcessPrinter : IPrinter
{
    private SubProcess s;

    public SubProcessPrinter(SubProcess subProcess)
    {
        s = subProcess;
    }

    public void Statistics()
    {
        Console.Out.WriteLine($"\t{s.Name}:");
        Console.WriteLine($"\t\tQuantity = {s.Quantity}");
        Console.WriteLine($"\t\tWorkTime = {IPrinter.Format(s.WorkTime)}");
    }

    public void Info()
    {
        if (s.IsWorking)
        {
            Console.Write($"\t{IPrinter.PrintState(s.IsWorking)}{s.Name}\t " +
                              $"quantity={s.Quantity}  " +
                              $"delay={IPrinter.Format(s.Delay)}  " +
                              $"tnext={IPrinter.Format(s.NextT)}  ");
            if(s.Item!.Name != "") Console.Write($"item={s.Item.Name}_{s.Item.Id}  ");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"\t{IPrinter.PrintState(s.IsWorking)}{s.Name}\t " +
                              $"quantity={s.Quantity} ");
        }
    }
}