using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.Statistics;

public class ModelStatisticHelper
{
    // Items Count
    public int Quantity => _model.Creates.Sum(e => e.BaseStatistic.OutActQuantity);
    public double AverageItemsCount => AverageItemsCountAllTime / Time.All;
    public double AverageItemsCountAllTime;
    
    // Failure
    public int FailureQuantity => _model.Processes.Sum(e => e.ProcessStatistic.Failure);
    public double FailurePercent => (double)FailureQuantity / Quantity * 100;

    // ItemsTime in system
    public double AverageItemTimeInSystem => AllFinishedItemsTimeInSystem / FinishedItemsCount;
    public int FinishedItemsCount { get; set; }
    public double AllFinishedItemsTimeInSystem { get; set; }
    
    private Model _model;
    
    public ModelStatisticHelper(Model model) => _model = model;
}