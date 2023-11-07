using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Items;
using MassServiceModeling.Printers;
using MassServiceModeling.Statistics;

namespace MassServiceModeling.Elements;

public class Create : Element
{
    public new CreateStatisticHelper StatisticHelper { get; }
    
    public Create(Randomizer randomizer, string name = "") : base(randomizer, name)
    {
        StatisticHelper = new CreateStatisticHelper(this);
        Print = new CreatePrinter(this);
    }

    public override void OutAct()
    {
        Item = CreateItem();
        base.OutAct();
    }

    public Create(double delay = 1.0, string name = "CREATE") : this(new ExponentialRandomizer(delay), name) {}

    protected virtual Item CreateItem() => new(Time.Curr);

    protected override void SetItem(Item item) => Item = item;

    protected override string GetElementName() => "CREATE";

    protected override void UpdateNextT() => Time.Next = Time.Curr + GetDelay();
}