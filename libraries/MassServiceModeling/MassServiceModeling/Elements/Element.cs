using DistributionRandomizer.DelayRandomizers;
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
    public bool IsWorking { protected set; get; }
    public IPrinter Print { get; protected init; }
    public NextElementsContainer? NextElementsContainer;


    public readonly string Name;
    protected readonly int Id = _nextId;
    private static int _nextId;
    private Randomizer _randomizer;

    protected Element(Randomizer randomizer, string name)
    {
        _randomizer = randomizer;
        Name += $"{name}_{Id}";
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
        NextElementsContainer?.InAct();
        UpdateNextT();
    }

    public virtual void DoStatistics(double delta)
    {
        WorkTime += IsWorking ? delta : 0;
    }


    protected double GetDelay() => _randomizer.GenerateDelay();

    protected abstract void UpdateNextT();
}