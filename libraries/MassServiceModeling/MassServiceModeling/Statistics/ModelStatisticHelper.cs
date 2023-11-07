namespace MassServiceModeling.Statistics;

public class ModelStatisticHelper
{
    // Quantity and Percent
    public int Quantity => _model.Creates.Sum(e => e.StatisticHelper.OutActQuantity);
    public int FailureQuantity => _model.Processes.Sum(e => e.StatisticHelper.Failure);
    public double FailurePercent => (double)FailureQuantity / Quantity * 100;
    public double AverageItemsCount => AverageItemsCountAllTime / _model.Time.All;
    public double AverageItemsCountAllTime;
    
    // Items time in system
    public double AverageItemTimeInSystem => AllFinishedItemsTimeInSystem / FinishedItemsCount;
    public int FinishedItemsCount { get; set; }
    public double AllFinishedItemsTimeInSystem { get; set; }
    
    private Model _model;
    
    public ModelStatisticHelper(Model model)
    {
        _model = model;
    }
}