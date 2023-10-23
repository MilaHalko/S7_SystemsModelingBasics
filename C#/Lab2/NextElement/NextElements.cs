using Lab2.Elements;

namespace Lab2.NextElement;

public class NextElements
{
    public List<NextElement> NextElementsList { get; set; } = new();

    public void AddNextElement(Element element, double probability = 1) => NextElementsList.Add(new NextElement(element, probability));

    private NextElement getNextElement()
    {
        double sum = NextElementsList.Sum(nextElement => nextElement.probability);
        double random = new Random().NextDouble() * sum;
        double current = 0;
        foreach (var nextElement in NextElementsList)
        {
            current += nextElement.probability;
            if (random < current)
                return nextElement;
        }
        throw new Exception("NextElement not found");
    }

    public void InAct()
    {
        getNextElement().nextElement.InAct();
    }
}
