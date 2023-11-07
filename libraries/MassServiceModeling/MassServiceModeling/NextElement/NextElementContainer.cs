using MassServiceModeling.Elements;

namespace MassServiceModeling.NextElement;

public class NextElementContainer : NextElementsContainer
{
    private readonly Element _nextElement;
    
    public NextElementContainer(Element nextElement)
    {
        _nextElement = nextElement;
    }
    
    protected override Element GetNextElement() => _nextElement;
}