namespace Lab1.Generators;

public class ExponentialGenerator : Generator
{
    public double Lambda { get; set; }

    private readonly Random _random = new Random();

    public ExponentialGenerator(double lambda = 1)
    {
        Lambda = lambda;
    }

    protected override double GenerateNumber() => (-1 / Lambda) * Math.Log(_random.NextDouble());

    public override Func<double, double, double> GetIntegralFunc() =>
        (start, end) => (1 - Math.Exp(-Lambda * end)) - (1 - Math.Exp(-Lambda * start));
}