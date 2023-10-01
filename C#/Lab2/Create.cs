namespace Lab2;

public class Create : Element
{
    public Create(double delay = 1.0, string name = "",  string distribution = "exp") : base(delay, name, distribution)
    {
        Tnext = 0.0;
    }

    public override void OutAct()
    {
        base.OutAct();
        Tnext = Tcurr + GetDelay();
        NextElement?.InAct();
    }
}