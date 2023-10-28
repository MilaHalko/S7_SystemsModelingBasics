using MassServiceModeling.Elements;

namespace MassServiceModeling.NextElement;

public class NextElementsContainerByProbability : NextElementsContainer
{
    private List<NextElement> NextElements { get; } = new();

    public void AddNextElement(Element element, double probability) => NextElements.Add(new NextElement(element, probability));

    protected override Element GetNextElement()
    {
        double sum = NextElements.Sum(nextElement => nextElement.Probability);
        double random = new Random().NextDouble() * sum;
        double current = 0;
        foreach (var nextElement in NextElements)
        {
            current += nextElement.Probability;
            if (random < current) return nextElement.Element;
        }
        throw new Exception("NextElement not found");
    }
}