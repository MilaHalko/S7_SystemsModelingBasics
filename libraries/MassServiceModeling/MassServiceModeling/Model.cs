using MassServiceModeling.Elements;
using MassServiceModeling.Printers;

namespace MassServiceModeling;

public class Model
{
    public List<Create> Creates => _elements.OfType<Create>().ToList();
    public List<Process> Processes => _elements.OfType<Process>().ToList();
    public int Quantity => Creates.Sum(e => e.QuantityProcessed);
    public int FailureQuantity => Processes.Sum(e => e.Failure);
    public double FailurePercent => (double)FailureQuantity / Quantity * 100;
    public double AverageItemsCount => AverageItemsCountAllTime / AllTime;
    protected double AverageItemsCountAllTime;

    public event Action? OnNextElementStarted;
    protected double Delta => NextT - CurrT;
    protected double AllTime => CurrT - _startTime;
    private double _startTime;
    protected bool InitialStateAccessed { get; }
    protected readonly List<Element> _elements;
    protected double NextT, CurrT;
    private int _event;

    public Model(List<Element> elements, bool initialStateIsNeeded = false)
    {
        _elements = elements;
        InitialStateAccessed = !initialStateIsNeeded;
    }

    public virtual void Simulate(double time, double startTime = 0, bool printSteps = false)
    {
        _startTime = startTime;
        while (CurrT < time)
        {
            DefineNextEvent();
            if (printSteps) IPrinter.PrintCurrent(_elements[_event]);
            if (InitialStateAccessed) DoStatistics();
            else OnNextElementStarted?.Invoke();

            ShiftTime();
            OutActForFinished();
            if (printSteps) IPrinter.Info(_elements);
        }

        IPrinter.Result(_elements);
    }

    protected virtual void DoStatistics()
    {
        _elements.ForEach(e => e.DoStatistics(Delta));
        AverageItemsCountAllTime += Processes.Sum(p => p.IsWorking ? p.Queue + 1 : 0) * Delta;
    }

    private void ShiftTime()
    {
        CurrT = NextT;
        _elements.ForEach(e => e.CurrT = CurrT);   
    }

    private void DefineNextEvent()
    {
        NextT = double.MaxValue;
        for (int i = 0; i < _elements.Count; i++)
        {
            if (_elements[i].NextT < NextT)
            {
                NextT = _elements[i].NextT;
                _event = i;
            }
        }
    }

    private void OutActForFinished()
    {
        foreach (var element in _elements)
            if (element.NextT == CurrT)
                element.OutAct();
    }
}