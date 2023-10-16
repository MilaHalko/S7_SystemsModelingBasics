namespace DistributionRandomizer.NumberRandomizers;

public class NormalRandomizer : Randomizer
{
    private readonly Random _random = new Random();
    private double Sigma { get; set; } // dispersion
    private double Alpha { get; set; } // mathematical expectation

    public NormalRandomizer(double alpha = 0, double sigma = 1)
    {
        Sigma = sigma;
        Alpha = alpha;
    }

    public override double GenerateNumber()
    {
        double u = Enumerable.Range(0, 12).Select(_ => _random.NextDouble()).Sum() - 6;
        return Sigma * u + Alpha;
    }
}