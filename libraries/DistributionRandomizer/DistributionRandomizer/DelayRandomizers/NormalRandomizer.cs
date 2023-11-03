namespace DistributionRandomizer.DelayRandomizers;

public class NormalRandomizer : Randomizer
{
    private double TimeMean { get; } // dispersion
    private double TimeDeviation { get; } // mathematical expectation

    public NormalRandomizer(double timeMean, double timeDeviation)
    {
        TimeMean = timeMean;
        TimeDeviation = timeDeviation;
    }

    public override double GenerateDelay()
    {
        double u = Enumerable.Range(0, 12).Select(_ => _random.NextDouble()).Sum() - 6;
        return TimeDeviation * u + TimeMean;
    }
}