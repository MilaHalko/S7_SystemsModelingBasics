using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Items;
using MassServiceModeling.Printers;
using MassServiceModeling.Statistics;
using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.Elements;

public class Create : Element
{
    public CreateStatisticHelper CreateStatistic = new();
    public Create(Randomizer randomizer, string name = "") : base(randomizer, name)
    {
        NextT = Time.Start;
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

    protected override string GetElementDefaultName() => "CREATE";

    protected override void UpdateNextT() => NextT = Time.Curr + GetDelay();
}