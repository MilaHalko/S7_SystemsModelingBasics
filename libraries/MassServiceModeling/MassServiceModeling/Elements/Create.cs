using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Printers;

namespace MassServiceModeling.Elements;

public class Create : Element
{
    public Create(Randomizer randomizer, string name = "CREATE") : base(randomizer, name)
    {
        Print = new CreatePrinter(this);
    }

    public Create(double delay = 1.0, string name = "CREATE") : this(new ExponentialRandomizer(delay), name) {}

    protected override void UpdateNextT()
    {
        NextT = CurrT + GetDelay();
    }
}