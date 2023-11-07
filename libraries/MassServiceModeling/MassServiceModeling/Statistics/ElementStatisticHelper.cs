using MassServiceModeling.Elements;

namespace MassServiceModeling.Statistics;

public class ElementStatisticHelper
{
    public double WorkTime { get; set; }

    public int InActQuantity { get; private set; }
    public int OutActQuantity { get; set; }
    public double TotalTimeBetweenInActs { get; private set; }
    public double TotalTimeBetweenOutActs { get; private set; }
    private double? _lastInActTime;
    private double? _lastOutActTime;
    
    private Element _element;
    
    public ElementStatisticHelper(Element element) => _element = element;


    public void InAct()
    {
        InActQuantity++;
        TotalTimeBetweenInActs += _element.Time.Curr - _lastInActTime ?? 0;
        _lastInActTime = _element.Time.Curr;
    }

    public void OutAct()
    {
        TotalTimeBetweenOutActs += _element.Time.Curr - _lastOutActTime ?? 0;
        _lastOutActTime = _element.Time.Curr;
    }
}