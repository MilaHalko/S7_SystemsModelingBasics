

using DistributionRandomizer.DelayRandomizers;

namespace Lab2.Elements;

public abstract class Element
{
    public double NextT { get; protected set; }
    public double CurrT { get; set; }
    private readonly double _delayMean;
    private readonly double _delayDeviation;
    public Element? NextElement { get; set; }
    private readonly Randomizer? _randomizer;

    private static int _nextId;
    private readonly int _id = _nextId;
    public readonly string Name;
    public int Quantity { get; private set; }
    protected int State { get; set; }


    protected Element(double delay, string name, string distribution = "exp")
    {
        _delayMean = delay;
        Name += $"{name}_{_id}";
        _randomizer = GetRandomizerByName(distribution);
        _nextId++;
    }

    protected double GetDelay()
    {
        if (_randomizer != null) return _randomizer.GenerateDelay();
        return _delayMean;
    }

    public virtual void InAct() {}

    public virtual void OutAct()
    {
        Quantity++;
    }

    public void PrintResult()
    {
        Console.WriteLine($"{Name} quantity = {Quantity}");
    }

    public virtual void PrintInfo()
    {
        Console.WriteLine($"{Name} state= {State} quantity = {Quantity} tnext= {NextT}");
    }

    public virtual void DoStatistics(double delta) {}

    private Randomizer GetRandomizerByName(string distribution)
    {
        switch (distribution.ToLower())
        {
            case "exp":
                return new ExponentialRandomizer(_delayMean);
            case "norm":
                return new NormalRandomizer(_delayMean, _delayDeviation);
            case "unif":
                return new UniformRandomizer(_delayMean, _delayDeviation);
            case "":
            case null:
                return null!;
            default:
                throw new ArgumentException("Unknown distribution");
        }
    }
}