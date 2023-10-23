namespace Lab2.Elements;

public class Create : Element
{
    public Create(double delay = 1.0, string distribution = "exp", string name = "CREATE") : 
        base(delay, name, distribution) { }

    protected override void UpdateNextT()
    {
        NextT = CurrT + GetDelay();
    }
}