using MassServiceModeling;
using MassServiceModeling.Elements;

namespace Lab3.Tasks.Task2;

public class Task2Model : Model
{
    public int QueueChangeCount7;
    public Task2Model(List<Element> elements) : base(elements) {}

    public override void Simulate(double time, double startTime = 0, bool printSteps = false)
    {
        base.Simulate(time, startTime, printSteps);
        Console.WriteLine();
        Console.WriteLine($"1) Average workTime:"); PrintEachProcessWorkTime_1();
        Console.WriteLine($"2) Average clients count: {AverageItemsCount}");
        Console.WriteLine($"3) Average time between departures: {AverageBetweenOutActs3}");
        Console.WriteLine($"4) Average time spent by a customer in the bank: {AverageItemTimeInSystem}");
        Console.WriteLine($"5) Average clients count in each queue:"); PrintEachProcessMeanQueue_5();
        Console.WriteLine($"6) Failure probability: {FailurePercent}%");
        Console.WriteLine($"7) Queue change count: {QueueChangeCount7}");
    }

    private void PrintEachProcessWorkTime_1() => Processes.ForEach(p => Console.WriteLine($"\t{p.Name}: {p.WorkTime / AllTime}"));
    private double AverageBetweenOutActs3 => Processes.Sum(p => p.TotalTimeBetweenOutActs / p.OutActQuantity) / Processes.Count;
    private void PrintEachProcessMeanQueue_5() => Processes.ForEach(p => Console.WriteLine($"\t{p.Name}: {p.MeanQueueAllTime / AllTime}"));
}