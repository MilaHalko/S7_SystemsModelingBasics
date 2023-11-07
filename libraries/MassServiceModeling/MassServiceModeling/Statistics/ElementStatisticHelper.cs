using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.Statistics;

public class ElementStatisticHelper
{
    public double WorkTime { get; set; }
    public int InActQuantity { get; private set; }
    public int OutActQuantity { get; private set; }
    public double TotalTimeBetweenInActs { get; private set; }
    public double TotalTimeBetweenOutActs { get; private set; }
    private double? _lastInActTime;
    private double? _lastOutActTime;
    
    public void InAct()
    {
        InActQuantity++;
        TotalTimeBetweenInActs += Time.Curr - _lastInActTime ?? 0;
        _lastInActTime = Time.Curr;
    }

    public void OutAct()
    {
        OutActQuantity++;
        TotalTimeBetweenOutActs += Time.Curr - _lastOutActTime ?? 0;
        _lastOutActTime = Time.Curr;
    }
}