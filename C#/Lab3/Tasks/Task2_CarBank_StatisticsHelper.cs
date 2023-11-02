using MassServiceModeling;
using MassServiceModeling.Elements;

namespace Lab3.Tasks;

public class Task2Model : Model
{
    public int QueueChangeCount_7;
    public Task2Model(List<Element> elements) : base(elements) {}

    public override void Simulate(double time, double startTime = 0, bool printSteps = false)
    {
        base.Simulate(time, startTime, printSteps);
        Console.WriteLine();
        Console.WriteLine($"1) Average workTime:"); PrintEachProcessWorkTime_1();
        Console.WriteLine($"2) Average clients count: {AverageItemsCount}");
        Console.WriteLine($"3) Average time between departures: {AverageBetweenOutActs_3}");
        Console.WriteLine($"4) Average time spent by a customer in the bank: {AverageTimeBeingInBank_4}");
        Console.WriteLine($"5) Average clients count in each queue:"); PrintEachProcessMeanQueue_5();
        Console.WriteLine($"6) Failure probability: {FailurePercent}%");
        Console.WriteLine($"7) Queue change count: {QueueChangeCount_7}");
    }

    private void PrintEachProcessWorkTime_1() => Processes.ForEach(p => Console.WriteLine($"\t{p.Name}: {p.WorkTime / AllTime}"));
    private double AverageBetweenOutActs_3 => Processes.Sum(p => p.TotalTimeBetweenOutActs / p.QuantityProcessed) / Processes.Count();
    private double AverageTimeBeingInBank_4 => Processes.Sum(p => p.WorkTime / p.QuantityProcessed * AverageItemsCount) / Processes.Count();
    private void PrintEachProcessMeanQueue_5() => Processes.ForEach(p => Console.WriteLine($"\t{p.Name}: {p.MeanQueueAllTime / AllTime}"));
}