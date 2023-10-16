namespace DistributionRandomizer.DelayRandomizers;

public class UniformRandomizer : Randomizer
{
    private double TimeMin { get; set; }
    private double TimeMax { get; set; }

    public UniformRandomizer(double timeMin, double timeMax)
    {
        TimeMin = timeMin;
        TimeMax = timeMax;
    }

    public override double GenerateDelay()
    {
        double a = 0;
        while(a == 0) a = new Random().NextDouble();
        return TimeMin + a * (TimeMax - TimeMin);
    }
}