using MassServiceModeling.Statistics;
using MassServiceModeling.SubProcesses;

namespace MassServiceModeling.Printers;

public class SubProcessPrinter : IPrinter
{
    private SubProcess sp;
    private SubProcessStatisticHelper s;

    public SubProcessPrinter(SubProcess subProcess)
    {
        sp = subProcess;
        s = subProcess.StatisticHelper;
    }

    public void Statistics()
    {
        Console.Out.WriteLine($"\t{sp.Name}:");
        Console.WriteLine($"\t\tQuantity = {s.Quantity}");
        Console.WriteLine($"\t\tWorkTime = {IPrinter.Format(s.WorkTime / sp.ProcessTime.Curr)}");
    }

    public void Info()
    {
        if (sp.IsWorking)
        {
            Console.Write($"\t{IPrinter.PrintState(sp.IsWorking)}{sp.Name}\t " +
                          $"quantity={s.Quantity}  " +
                          $"delay={IPrinter.Format(sp.Time.Delay)}  " +
                          $"tnext={IPrinter.Format(sp.Time.Next)}  " +
                          $"{IPrinter.GetItemName(sp.Item!)}  ");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"\t{IPrinter.PrintState(sp.IsWorking)}{sp.Name}\t " +
                              $"quantity={s.Quantity} ");
        }
    }
}