using MathNet.Numerics;
using static System.Math;

namespace Lab1.Generators;

public class NormalGenerator : Generator
{
    private readonly Random _random = new Random();
    public double Sigma { get; set; } // dispersion
    public double Alpha { get; set; } // mathematical expectation

    public NormalGenerator(double sigma = 1, double alpha = 0)
    {
        Sigma = sigma;
        Alpha = alpha;
    }

    protected override double GenerateNumber()
    {
        double u = Enumerable.Range(0, 12).Select(_ => _random.NextDouble()).Sum() - 6;
        return Sigma * u + Alpha;
    }

    public override Func<double, double, double> GetIntegralFunc() => (start, end) =>
    {
        double Func(double x) => 1 / (Sigma * Sqrt(2 * PI)) * Exp(-(Pow(x - Alpha, 2) / (2 * Pow(Sigma, 2))));
        return Integrate.OnClosedInterval(Func, start, end);
    };
}