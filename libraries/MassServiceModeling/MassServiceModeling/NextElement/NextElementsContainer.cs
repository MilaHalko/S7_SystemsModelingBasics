using MassServiceModeling.Elements;
using MassServiceModeling.Items;

namespace MassServiceModeling.NextElement;

public class NextElement
{
    public readonly Element Element;
    public readonly double Probability;
    public NextElement(Element element, double probability = 1)
    {
        Element = element;
        Probability = probability;
    }
}

public abstract class NextElementsContainer
{
    public void InAct(Item item) => GetNextElement().InAct(item);
    protected abstract Element GetNextElement();
}