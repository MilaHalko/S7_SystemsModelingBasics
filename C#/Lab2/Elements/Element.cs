using DistributionRandomizer.DelayRandomizers;
using Lab2.NextElement;
using Lab2.Print;

namespace Lab2.Elements;

public abstract class Element
{
    public double NextT { get; protected set; }
    public double CurrT { get; set; }
    public int Quantity { get; private set; }
    public int QuantityProcessed { get; private set; }
    public double WorkTime { get; private set; }
    public bool IsWorking { private set; get; }
    public IPrinter Print { get; protected init; }
    public readonly string Name;

    private NextElements? _nextElement;
    private readonly double _delayMean;
    private readonly double _delayDeviation;
    private readonly Randomizer? _randomizer;

    protected readonly int Id = _nextId;
    private static int _nextId;

    protected Element(double delay, string name, string distribution = "exp")
    {
        _delayMean = delay;
        Name += $"{name}_{Id}";
        _randomizer = GetRandomizerByName(distribution);
        _nextId++;
        Print = new ElementPrinter(this);
    }

    public void SetNextElement(Element element, double probability = 1)
    {
        _nextElement ??= new NextElements();
        _nextElement.AddNextElement(element, probability);
    }


    public virtual void InAct()
    {
        Quantity++;
        IsWorking = true;
    }

    public virtual void OutAct()
    {
        QuantityProcessed++;
        IsWorking = false;
        _nextElement?.InAct();
        UpdateNextT();
    }

    public virtual void DoStatistics(double delta)
    {
        WorkTime += IsWorking ? delta : 0;
    }


    protected double GetDelay()
    {
        if (_randomizer != null) return _randomizer.GenerateDelay();
        return _delayMean;
    }

    protected abstract void UpdateNextT();


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