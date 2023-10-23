using Lab2.Elements;

namespace Lab2.NextElement;

public class NextElements
{
    private List<NextElement> NextElementsList { get; } = new();

    public void AddNextElement(Element element, double probability = 1) => NextElementsList.Add(new NextElement(element, probability));

    private NextElement GetNextElement()
    {
        double sum = NextElementsList.Sum(nextElement => nextElement.Probability);
        double random = new Random().NextDouble() * sum;
        double current = 0;
        foreach (var nextElement in NextElementsList)
        {
            current += nextElement.Probability;
            if (random < current)
                return nextElement;
        }
        throw new Exception("NextElement not found");
    }

    public void InAct()
    {
        GetNextElement().Element.InAct();
    }
}
