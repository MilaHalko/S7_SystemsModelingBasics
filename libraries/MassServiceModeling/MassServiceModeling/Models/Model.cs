using MassServiceModeling.Elements;
using MassServiceModeling.Printers;
using MassServiceModeling.Statistics;
using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.Models;

public class Model
{
    // Elements collections
    public List<Create> Creates => Elements.OfType<Create>().ToList();
    public List<Process> Processes => Elements.OfType<Process>().ToList();
    protected readonly List<Element> Elements;
    
    public event Action? OnNextElementStarted;
    public ModelStatisticHelper StatisticHelper;
    protected bool InitialStateAccessed { get; }
    protected double NextT;
    private int _event;

    public Model(List<Element> elements, bool initialStateIsNeeded = false)
    {
        Elements = elements;
        StatisticHelper = new ModelStatisticHelper(this);
        InitialStateAccessed = !initialStateIsNeeded;
        Elements.ForEach(e => e.Model = this);
    }

    public virtual void Simulate(double time, double startTime = 0, bool printSteps = false, bool printResult = true)
    {
        Time.SetStart(startTime);
        
        while (Time.Curr < time)
        {
            DefineNextEvent();
            if (InitialStateAccessed) DoStatistics();
            else OnNextElementStarted?.Invoke();
            Time.ShiftCurr(NextT);
            OutActForFinished();
            if (printSteps)
            {
                IPrinter.PrintCurrent(Elements[_event]);
                IPrinter.Info(Elements, Elements[_event]);
            }
        }
        if (printResult) IPrinter.Result(Elements);
    }

    protected virtual void DoStatistics()
    {
        Elements.ForEach(e => e.DoStatistics(Time.Delta(NextT)));
        StatisticHelper.AverageItemsCountAllTime += Processes.Sum(p => p.IsWorking ? p.Queue.Length + 1 : 0) * Time.Delta(NextT);
    }

    public void AddItemTimeInSystem(double timeInSystem)
    {
        StatisticHelper.FinishedItemsCount++;
        StatisticHelper.AllFinishedItemsTimeInSystem += timeInSystem;
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
        foreach (var element in Elements.Where(element => element.NextT == Time.Curr))
            element.OutAct();
    }
}