namespace Lab1.Generators;

public class UniformGenerator : Generator
{
    public double A { get; set; } // 5^13 == 1220703125
    public double C { get; set; } // 2^31 == 2147483648
    public double Z { get; set; }

    public UniformGenerator(double a = 1220703125, double c = 2147483648)
    {
        A = a;
        C = c;
        Z = new Random().NextDouble();
    }

    protected override double GenerateNumber()
    {
        Z = (A * Z) % C;
        return (double)Z / C;
    }

    public override Func<double, double, double> GetIntegralFunc() => (start, end) => end - start;
}