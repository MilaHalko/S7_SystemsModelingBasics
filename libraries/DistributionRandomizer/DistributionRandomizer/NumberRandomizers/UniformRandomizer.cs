namespace DistributionRandomizer.NumberRandomizers;

public class UniformRandomizer : Randomizer
{
    private double A { get; set; } // 5^13 == 1220703125
    private double C { get; set; } // 2^31 == 2147483648
    private double Z { get; set; }

    public UniformRandomizer(double a = 1220703125, double c = 2147483648)
    {
        A = a;
        C = c;
        Z = new Random().NextDouble();
    }

    public override double GenerateNumber()
    {
        Z = (A * Z) % C;
        return Z / C;
    }
}