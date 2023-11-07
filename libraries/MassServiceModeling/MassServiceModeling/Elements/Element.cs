using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Items;
using MassServiceModeling.NextElement;
using MassServiceModeling.Printers;
using MassServiceModeling.Statistics;
using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.Elements;

public abstract class Element
{
    // Non-static attributes
    public bool IsWorking { get; protected set; }
    public Item? Item { get; protected set; }
    public double NextT = double.MaxValue;
    public double Delay;

    // Static attributes
    public NextElementsContainer? NextElementsContainer;
    public Model? Model { get; set; }
    public ElementStatisticHelper BaseStatistic = new();
    public IPrinter Print { get; protected init; }
    public Randomizer Randomizer { get; }

    // Self-static
    public string Name { get; }
    private static int _nextId;
    public int Id { get; } = _nextId++;

    protected Element(Randomizer randomizer, string name)
    {
        Name = name == "" ? $"{GetElementDefaultName()}_{Id}" : name;
        Print = new ElementPrinter(this);
        Randomizer = randomizer;
    }

    public virtual void InAct(Item item)
    {
        IsWorking = true;
        DoInActStatistics();
        SetItem(item);
    }
    
    public virtual void OutAct()
    {
        IsWorking = false;
        DoOutActStatistics();
        NextElementsContainer?.InAct(Item ?? throw new InvalidOperationException());
        Item = null;
        UpdateNextT();
    }
    
    public virtual void DoStatistics(double delta) => BaseStatistic.WorkTime += IsWorking ? delta : 0;

    protected virtual double GetDelay() => Delay = Randomizer.GenerateDelay();

    private void DoInActStatistics() => BaseStatistic.InAct();

    private void DoOutActStatistics()
    {
        if (Item is null) {throw new InvalidOperationException();}
        IPrinter.CurrentItem = Item;
        Model!.AddItemTimeInSystem(Time.Curr - Item.StartTime);
        BaseStatistic.OutAct();
    }

    protected abstract void UpdateNextT();

    protected abstract void SetItem(Item item);

    protected abstract string GetElementDefaultName();
}