namespace DistributionRandomizer.DelayRandomizers;

public class NormalRandomizer : Randomizer
{
    private readonly Random _random = new Random();
    private double TimeMean { get; set; } // dispersion
    private double TimeDeviation { get; set; } // mathematical expectation

    public NormalRandomizer(double timeMean, double timeDeviation)
    {
        this.TimeMean = timeMean;
        this.TimeDeviation = timeDeviation;
    }

    public override double GenerateDelay()
    {
        double u = Enumerable.Range(0, 12).Select(_ => _random.NextDouble()).Sum() - 6;
        return TimeDeviation * u + TimeMean;
    }
}