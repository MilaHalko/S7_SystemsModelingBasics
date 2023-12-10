using Lab2.Elements;
namespace Lab2.NextElement;

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