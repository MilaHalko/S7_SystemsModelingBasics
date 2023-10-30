namespace DistributionRandomizer.DelayRandomizers;

public class ExponentialRandomizer : Randomizer
{
    private double TimeMean { get; }
    private readonly Random _random = new();

    public ExponentialRandomizer(double timeMean)
    {
        TimeMean = timeMean;
    }

    public override double GenerateDelay() => -TimeMean * Math.Log(_random.NextDouble());
}