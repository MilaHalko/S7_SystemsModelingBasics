namespace DistributionRandomizer.DelayRandomizers;

public class ExponentialRandomizer : Randomizer
{
    private double TimeMean { get; }

    public ExponentialRandomizer(double timeMean)
    {
        TimeMean = timeMean;
    }

    public override double GenerateDelay() => -TimeMean * Math.Log(_random.NextDouble());
    
    public static double GenerateDelay(double timeMean) => -timeMean * Math.Log(new Random().NextDouble());
}