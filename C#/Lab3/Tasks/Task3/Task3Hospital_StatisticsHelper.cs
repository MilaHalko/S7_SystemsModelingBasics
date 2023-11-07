using MassServiceModeling;
using MassServiceModeling.Elements;

namespace Lab3.Tasks.Task3;

public class Task3HospitalModel : Model
{
    public Task3HospitalModel(List<Element> elements, bool initialStateIsNeeded = false) : base(elements, initialStateIsNeeded) {}

    public override void Simulate(double time, double startTime = 0, bool printSteps = false)
    {
        base.Simulate(time, startTime, printSteps);
        Console.WriteLine();
        Console.WriteLine($"1) Average spent time in system: {StatisticHelper.AverageItemTimeInSystem}");
        Console.WriteLine($"2) Average time between arrivals in registry: {AverageBetweenInActs_2()}");
    }

    private double AverageBetweenInActs_2()
    {
        var lab = Elements.OfType<Process>().First(p => p.Name == "Registry");
        return lab.StatisticHelper.TotalTimeBetweenInActs / lab.StatisticHelper.InActQuantity;
    }
}