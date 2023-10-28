using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Enums;
using MassServiceModeling.NextElement;
using MassServiceModeling.Printers;

namespace MassServiceModeling.Elements;

public abstract class Element
{
    public double NextT { get; set; }
    public double CurrT { get; set; }
    public int Quantity { get; private set; }
    public int QuantityProcessed { get; private set; }
    public double WorkTime { get; private set; }
    public bool IsWorking { private set; get; }
    public IPrinter Print { get; protected init; }
    public readonly string Name;

    public NextElementsContainer? _nextElementsContainer;
    private readonly double _delayMean;
    private readonly double _delayDeviation;
    private readonly Randomizer? _randomizer;

    protected readonly int Id = _nextId;
    private static int _nextId;

    // TODO: remove NextElementsContainer from constructor
    protected Element(double delay, string name, NextElementsContainer nextElementsContainer = null, Distribution distribution = Distribution.Exponential)
    {
        _delayMean = delay;
        Name += $"{name}_{Id}";
        _nextElementsContainer = nextElementsContainer;
        _randomizer = GetRandomizer(distribution);
        _nextId++;
        Print = new ElementPrinter(this);
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
        _nextElementsContainer?.InAct();
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


    private Randomizer GetRandomizer(Distribution distribution)
    {
        switch (distribution)
        {
            case Distribution.Exponential: return new ExponentialRandomizer(_delayMean);
            case Distribution.Normal: return new NormalRandomizer(_delayMean, _delayDeviation);
            case Distribution.Uniform: return new UniformRandomizer(_delayMean, _delayDeviation);
            default: throw new ArgumentOutOfRangeException(nameof(distribution), distribution, null);
        }
    }
}