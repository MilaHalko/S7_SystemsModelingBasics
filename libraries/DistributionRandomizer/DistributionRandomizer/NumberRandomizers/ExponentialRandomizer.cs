namespace DistributionRandomizer.NumberRandomizers;

public class ExponentialRandomizer : Randomizer
{
    private double Lambda { get; set; }
    private readonly Random _random = new Random();

    public ExponentialRandomizer(double lambda = 1)
    {
        Lambda = lambda;
    }

    public override double GenerateNumber() => (-1 / Lambda) * Math.Log(_random.NextDouble());
}