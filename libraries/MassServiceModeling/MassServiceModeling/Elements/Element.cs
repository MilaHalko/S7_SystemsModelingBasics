using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.NextElement;
using MassServiceModeling.Printers;

namespace MassServiceModeling.Elements;

public abstract class Element
{
    // Statistics
    public double WorkTime { get; private set; }

    // Statistics: InAct && OutAct statistics 
    public int InActQuantity { get; private set; }
    public int OutActQuantity { get; private set; }
    public double TotalTimeBetweenInActs { get; private set; }
    public double TotalTimeBetweenOutActs { get; private set; }
    private double? _lastInActTime;
    private double? _lastOutActTime;
    
    // Non-static attributes
    public double CurrT { get; set; }
    public double NextT { get; set; }
    public Item? Item { get; protected set; }
    public bool IsWorking { get; protected set; }
    public double Delay { get; private set; }

    // Static attributes
    public NextElementsContainer? NextElementsContainer;
    public Model? Model { get; set; }
    public IPrinter Print { get; protected init; }
    public Randomizer Randomizer { get; }

    // Self-static
    public string Name { get; }
    private static int _nextId;
    public int Id { get; } = _nextId++;

    protected Element(Randomizer randomizer, string name)
    {
        Name = name == "" ? $"{GetElementName()}_{Id}" : name;
        Print = new ElementPrinter(this);
        Randomizer = randomizer;
    }

    public virtual void InAct(Item item)
    {
        IsWorking = true;
        DoInActsStatistics();
        SetItem(item);
    }
    
    public virtual void OutAct()
    {
        IsWorking = false;
        DoOutActsStatistics();
        NextElementsContainer?.InAct(Item ?? throw new InvalidOperationException());
        Item = null;
        UpdateNextT();
    }
    
    public virtual void DoStatistics(double delta)
    {
        WorkTime += IsWorking ? delta : 0;
    }

    protected virtual double GetDelay() => Delay = Randomizer.GenerateDelay();

    private void DoInActsStatistics()
    {
        InActQuantity++;
        TotalTimeBetweenInActs += CurrT - _lastInActTime ?? 0;
        _lastInActTime = CurrT;
    }

    private void DoOutActsStatistics()
    {
        OutActQuantity++;
        
        if (Item is null) throw new InvalidOperationException();
        IPrinter.CurrentItem = Item;
        Model!.AddItemTimeInSystem(CurrT - Item.StartTime);
        
        TotalTimeBetweenOutActs += CurrT - _lastOutActTime ?? 0;
        _lastOutActTime = CurrT;
    }

    protected abstract void UpdateNextT();

    protected abstract void SetItem(Item item);

    protected abstract string GetElementName();
}