namespace Lab1.Generators;

public class UniformGenerator : Generator
{
    public long A { get; set; } // 5^13 == 1220703125
    public long C { get; set; } // 2^31 == 2147483648
    public long Z { get; set; }

    public UniformGenerator(long a = 1220703125, long c = 2147483648)
    {
        A = a;
        C = c;
        Z = new Random().Next();
    }

    protected override double GenerateNumber()
    {
        Z = A * Z % C;
        return (double)Z / C;
    }

    public override Func<double, double, double> GetIntegralFunc() => (start, end) => end - start;
}