using MassServiceModeling.Elements;
using MassServiceModeling.Printers;

namespace MassServiceModeling;

public class Model
{
    // Elements collections
    public List<Create> Creates => Elements.OfType<Create>().ToList();
    public List<Process> Processes => Elements.OfType<Process>().ToList();
    protected readonly List<Element> Elements;

    // Quantity and Percent
    public int Quantity => Creates.Sum(e => e.OutActQuantity);
    public int FailureQuantity => Processes.Sum(e => e.Failure);
    public double FailurePercent => (double)FailureQuantity / Quantity * 100;
    public double AverageItemsCount => AverageItemsCountAllTime / AllTime;
    protected double AverageItemsCountAllTime;

    // Items time in system
    public double AverageItemTimeInSystem => AllFinishedItemsTimeInSystem / FinishedItemsCount;
    public int FinishedItemsCount { get; private set; }
    public double AllFinishedItemsTimeInSystem { get; private set; }

    // Time attributes
    protected double NextT, CurrT;
    protected double Delta => NextT - CurrT;
    protected double AllTime => CurrT - _startTime;
    private double _startTime;

    public event Action? OnNextElementStarted;
    protected bool InitialStateAccessed { get; }
    private int _event;

    public Model(List<Element> elements, bool initialStateIsNeeded = false)
    {
        Elements = elements;
        InitialStateAccessed = !initialStateIsNeeded;
        Elements.ForEach(e => e.Model = this);
    }

    public virtual void Simulate(double time, double startTime = 0, bool printSteps = false)
    {
        _startTime = startTime;
        while (CurrT < time)
        {
            DefineNextEvent();
            if (InitialStateAccessed) DoStatistics();
            else OnNextElementStarted?.Invoke();
            ShiftTime();
            OutActForFinished();
            if (printSteps)
            {
                IPrinter.PrintCurrent(Elements[_event]);
                IPrinter.Info(Elements, Elements[_event]);
            }
        }

        IPrinter.Result(Elements);
    }

    protected virtual void DoStatistics()
    {
        Elements.ForEach(e => e.DoStatistics(Delta));
        AverageItemsCountAllTime += Processes.Sum(p => p.IsWorking ? p.QueueLength + 1 : 0) * Delta;
    }

    public void AddItemTimeInSystem(double timeInSystem)
    {
        FinishedItemsCount++;
        AllFinishedItemsTimeInSystem += timeInSystem;
    }

    private void ShiftTime()
    {
        CurrT = NextT;
        Elements.ForEach(e => e.CurrT = CurrT);
    }

    private void DefineNextEvent()
    {
        NextT = double.MaxValue;
        for (var i = 0; i < Elements.Count; i++)
        {
            if (!(Elements[i].NextT < NextT)) continue;
            NextT = Elements[i].NextT;
            _event = i;
        }
    }

    private void OutActForFinished()
    {
        foreach (var element in Elements.Where(element => element.NextT == CurrT))
            element.OutAct();
    }
}