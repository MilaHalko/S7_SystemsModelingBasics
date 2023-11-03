namespace DistributionRandomizer.DelayRandomizers;

public class ErlangRandomizer : Randomizer
{
    private double TimeMean { get; }
    private int k { get; }

    public ErlangRandomizer(double timeMean, int k)
    {
        TimeMean = timeMean;
        this.k = k;
    }
    
    public override double GenerateDelay() => Enumerable.Range(0, k).Select(_ => -TimeMean * Math.Log(_random.NextDouble())).Sum();
}