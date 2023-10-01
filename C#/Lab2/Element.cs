namespace Lab2;

public class Element
{
    public double Tnext { get; protected set; } // час наступної події
    public double Tcurr { get; set; }           // поточний час моделювання
    private readonly double _delayMean;         // середнє значення часу обслуговування
    private readonly double _delayDev;          // середнє квадратичне відхилення
    public Element? NextElement { get; set; }

    private readonly int _id;
    private static int _nextId;
    public readonly string Name;
    private readonly string _distribution;
    public int Quantity { get; private set; }
    protected int State { get; set; }


    protected Element(double delay, string name,  string distribution)
    {
        Tnext = 0.0;
        Tcurr = Tnext;
        _delayMean = delay;

        _id = _nextId;
        _nextId++;
        Name = name == "" ? $"element_{_id}" : $"{name}_{_id}";
        _distribution = distribution;
        State = 0;
    }

    protected double GetDelay()
    {
        double delay = _delayMean;
        switch (_distribution.ToLower())
        {
            case "exp":
                delay = FunRand.Exponential(_delayMean);
                break;
            case "norm":
                delay = FunRand.Normal(_delayMean, _delayDev);
                break;
            case "unif":
                delay = FunRand.Uniform(_delayMean, _delayDev);
                break;
            case "":
            case null:
                delay = _delayMean;
                break;
        }

        return delay;
    }

    public virtual void InAct() {}

    public virtual void OutAct()
    {
        Quantity++;
    }

    public void PrintResult()
    {
        Console.WriteLine($"{Name} quantity = {Quantity}");
    }

    public virtual void PrintInfo()
    {
        Console.WriteLine($"{Name} state= {State} quantity = {Quantity} tnext= {Tnext}");
    }

    public virtual void DoStatistics(double delta) {}
}