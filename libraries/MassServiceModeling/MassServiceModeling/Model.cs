using MassServiceModeling.Elements;
using MassServiceModeling.Printers;
using MassServiceModeling.Statistics;
using MassServiceModeling.Time;

namespace MassServiceModeling;

public class Model
{
    // Elements collections
    public List<Create> Creates => Elements.OfType<Create>().ToList();
    public List<Process> Processes => Elements.OfType<Process>().ToList();
    protected readonly List<Element> Elements;

    public ModelStatisticHelper StatisticHelper;
    public Time.Time Time = new();

    public event Action? OnNextElementStarted;
    protected bool InitialStateAccessed { get; }
    private int _event;

    public Model(List<Element> elements, bool initialStateIsNeeded = false)
    {
        Elements = elements;
        StatisticHelper = new ModelStatisticHelper(this);
        InitialStateAccessed = !initialStateIsNeeded;
        Elements.ForEach(e => e.Model = this);
    }

    public virtual void Simulate(double time, double startTime = 0, bool printSteps = false)
    {
        Time.Start = startTime;
        while (Time.Curr < time)
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
        Elements.ForEach(e => e.DoStatistics(Time.Delta));
        StatisticHelper.AverageItemsCountAllTime += Processes.Sum(p => p.IsWorking ? p.Queue.Length + 1 : 0) * Time.Delta;
    }

    public void AddItemTimeInSystem(double timeInSystem)
    {
        StatisticHelper.FinishedItemsCount++;
        StatisticHelper.AllFinishedItemsTimeInSystem += timeInSystem;
    }

    private void ShiftTime()
    {
        Time.ShiftTime();
        Elements.ForEach(e => e.Time.Curr = Time.Curr);
    }

    private void DefineNextEvent()
    {
        Time.Next = double.MaxValue;
        for (var i = 0; i < Elements.Count; i++)
        {
            if (!(Elements[i].Time.Next < Time.Next)) continue;
            Time.Next = Elements[i].Time.Next;
            _event = i;
        }
    }

    private void OutActForFinished()
    {
        foreach (var element in Elements.Where(element => element.Time.Next == Time.Curr))
            element.OutAct();
    }
}