namespace DistributionRandomizer.DelayRandomizers;

public class ExponentialRandomizer : Randomizer
{
    private double TimeMean { get; set; }
    private readonly Random _random = new Random();

    public ExponentialRandomizer(double timeMean)
    {
        TimeMean = timeMean;
    }

    public override double GenerateDelay() => -TimeMean * Math.Log(_random.NextDouble());
}