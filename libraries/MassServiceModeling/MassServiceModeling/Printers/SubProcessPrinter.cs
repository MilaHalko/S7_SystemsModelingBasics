using MassServiceModeling.Statistics;
using MassServiceModeling.SubProcesses;
using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.Printers;

public class SubProcessPrinter : IPrinter
{
    private SubProcess sp;
    private SubProcessStatisticHelper s => sp.StatisticHelper;

    public SubProcessPrinter(SubProcess subProcess) => sp = subProcess;

    public void Statistics()
    {
        Console.Out.WriteLine($"\t{sp.Name}:");
        Console.WriteLine($"\t\tQuantity = {s.Quantity}");
        Console.WriteLine($"\t\tWorkTime = {IPrinter.Format(s.WorkTime / Time.Curr)}");
    }

    public void Info()
    {
        if (sp.IsWorking)
        {
            Console.Write($"\t{IPrinter.PrintState(sp.IsWorking)}{sp.Name}\t " +
                          $"quantity={s.Quantity}  " +
                          $"delay={IPrinter.Format(sp.Delay)}  " +
                          $"quantity={s.Quantity}  " +
                          $"tnext={IPrinter.Format(sp.NextT)}  " +
                          $"{IPrinter.GetItemName(sp.Item!)}  ");
            Console.WriteLine();
        }
        else
            Console.WriteLine($"\t{IPrinter.PrintState(sp.IsWorking)}{sp.Name}\t " +
                              $"quantity={s.Quantity} ");
    }
}