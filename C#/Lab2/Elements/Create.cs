using Lab2.Elements;

namespace Lab2;

public class Create : Element
{
    public Create(double delay = 1.0, string distribution = "exp", string name = "CREATE") : 
        base(delay, name, distribution) { }


    public override void OutAct()
    {
        base.OutAct();
        NextT = CurrT + GetDelay();
        NextElement?.InAct();
    }

}