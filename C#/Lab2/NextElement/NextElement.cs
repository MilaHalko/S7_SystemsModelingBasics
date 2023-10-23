using Lab2.Elements;
namespace Lab2.NextElement;

public struct NextElement
{
    public Element nextElement;
    public double probability;

    public NextElement(Element element, double probability)
    {
        nextElement = element;
        this.probability = probability;
    }
}