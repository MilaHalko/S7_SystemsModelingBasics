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

    // Static attributes
    public Time Time = new();
    public NextElementsContainer? NextElementsContainer;
    public Model? Model { get; set; }
    public ElementStatisticHelper StatisticHelper;
    public IPrinter Print { get; protected init; }
    public Randomizer Randomizer { get; }

    // Self-static
    public string Name { get; }
    private static int _nextId;
    public int Id { get; } = _nextId++;

    protected Element(Randomizer randomizer, string name)
    {
        Name = name == "" ? $"{GetElementDefaultName()}_{Id}" : name;
        StatisticHelper = new ElementStatisticHelper(this);
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
    
    public virtual void DoStatistics(double delta) => StatisticHelper.WorkTime += IsWorking ? delta : 0;

    protected virtual double GetDelay() => Time.Delay = Randomizer.GenerateDelay();

    private void DoInActsStatistics() => StatisticHelper.InAct();

    private void DoOutActsStatistics()
    {
        StatisticHelper.OutActQuantity++;
        
        if (Item is null) throw new InvalidOperationException();
        IPrinter.CurrentItem = Item;
        Model!.AddItemTimeInSystem(Time.Curr - Item.StartTime);

        StatisticHelper.OutAct();
    }

    protected abstract void UpdateNextT();

    protected abstract void SetItem(Item item);

    protected abstract string GetElementDefaultName();
}