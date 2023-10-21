

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
    protected readonly int _id = _nextId;
    public readonly string Name;
    
    public int Quantity { get; private set; }
    public double WorkTime { get; private set; } 
    // TODO: Remove State
    protected bool IsWorking { get; set; }


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

    public virtual void InAct()
    {
        IsWorking = true;
    }

    public virtual void OutAct()
    {
        IsWorking = false;
        NextElement?.InAct();
        Quantity++;
    }

    public virtual void PrintInfo()
    {
        string nextTString = NextT == double.MaxValue ? "\u221E" : NextT.ToString();
        Console.WriteLine($"{Name} state = {IsWorking} quantity = {Quantity} tnext= {nextTString}");
    }

    public virtual void DoStatistics(double delta)
    {
        WorkTime += IsWorking? delta : 0;
        // Console.Out.WriteLine($"Total workTime of {Name} = " + WorkTime);
    }

    public void PrintFinalStatistics()
    {
        Console.Out.WriteLine($"{Name}:");
        Console.WriteLine($"\tQuantity = {Quantity}");
    }

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
    
    public static string NextTString(double nextT) => nextT == double.MaxValue ? "\u221E" : nextT.ToString();
}