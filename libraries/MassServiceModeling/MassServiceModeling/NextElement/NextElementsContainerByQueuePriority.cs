using MassServiceModeling.Elements;
namespace MassServiceModeling.NextElement;

public class NextElementsContainerByQueuePriority : NextElementsContainer
{
    public void AddNextElement(Process process, int priority) => _nextElements.Add(process, priority);
    private Dictionary<Process, int> _nextElements = new();
    protected override Element GetNextElement()
    {
        var elementsWithMinQueueLength = GetElementsWithMinQueueLength();
        return elementsWithMinQueueLength.OrderBy(e => _nextElements[e]).First();
    }

    private List<Process> GetElementsWithMinQueueLength()
    {
        var minQueueLength = _nextElements.Min(e => e.Key.Queue);
        var elementsWithMinQueueLength = new List<Process>();
        foreach (var nextElement in _nextElements)
        {
            if (nextElement.Key.Queue == minQueueLength)
            {
                elementsWithMinQueueLength.Add(nextElement.Key);
            }
        }
        return elementsWithMinQueueLength;
    }
}