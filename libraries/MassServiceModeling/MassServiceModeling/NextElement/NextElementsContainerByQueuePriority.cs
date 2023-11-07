using MassServiceModeling.Elements;
namespace MassServiceModeling.NextElement;

public class NextElementsContainerByQueuePriority : NextElementsContainer
{
    private Dictionary<Process, int> _nextElements = new();
    public void AddNextElement(Process process, int ascendingPriority) => _nextElements.Add(process, ascendingPriority);

    protected override Element GetNextElement()
    {
        var elementsWithMinQueueLength = GetElementsWithMinQueueLength();
        return elementsWithMinQueueLength.MinBy(e => e.Value).Key;
    }

    private Dictionary<Process, int> GetElementsWithMinQueueLength()
    {
        var minQueueLength = _nextElements.Min(e => e.Key.Queue.Length);
        var nextElementsWithMinQueueLength = new Dictionary<Process, int>();
        foreach (var (process, priority) in _nextElements)
            if (process.Queue.Length == minQueueLength)
                nextElementsWithMinQueueLength.Add(process, priority);
        return nextElementsWithMinQueueLength;
    }
}