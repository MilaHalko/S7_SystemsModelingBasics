using MassServiceModeling.Elements;

namespace MassServiceModeling.Statistics;

public class ProcessStatisticHelper : ElementStatisticHelper 
{
    // Statistics
    public int Failure { get; set; }
    public double MeanQueueAllTime { get; set; }
    
    public ProcessStatisticHelper(Element element) : base(element) {}
}