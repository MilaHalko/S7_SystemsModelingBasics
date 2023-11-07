using MassServiceModeling.Elements;

namespace MassServiceModeling.Statistics;

public class ProcessStatisticHelper : ElementStatisticHelper 
{
    public int Failure { get; set; }
    public double MeanQueueAllTime { get; set; }
    
    public ProcessStatisticHelper(Element element) : base(element) {}
}