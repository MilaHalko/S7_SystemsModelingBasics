using MassServiceModeling.Enums;
using MassServiceModeling.Printers;

namespace MassServiceModeling.Elements;

public class Create : Element
{
    public Create(double delay = 1.0, Distribution distribution = Distribution.Exponential, string name = "CREATE") :
        base(delay, name, distribution)
    {
        Print = new CreatePrinter(this);
    }

    protected override void UpdateNextT()
    {
        NextT = CurrT + GetDelay();
    }
}