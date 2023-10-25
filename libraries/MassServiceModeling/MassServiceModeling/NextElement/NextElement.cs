using MassServiceModeling.Elements;

namespace MassServiceModeling.NextElement;

public struct NextElement
{
    public readonly Element Element;
    public readonly double Probability;

    public NextElement(Element element, double probability)
    {
        Element = element;
        Probability = probability;
    }
}