using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Printers;

namespace MassServiceModeling.Elements;

public class Create : Element
{
    public Create(Randomizer randomizer, string name = "") : base(randomizer, name)
    {
        Print = new CreatePrinter(this);
    }

    public override void OutAct()
    {
        Item = CreateItem();
        base.OutAct();
    }

    public Create(double delay = 1.0, string name = "CREATE") : this(new ExponentialRandomizer(delay), name) {}

    protected virtual Item CreateItem() => new(CurrT);

    protected override void SetItem(Item item) => Item = item;

    protected override string GetElementName() => "CREATE";

    protected override void UpdateNextT() => NextT = CurrT + GetDelay();
}