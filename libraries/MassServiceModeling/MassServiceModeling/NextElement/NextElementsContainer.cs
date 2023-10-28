using MassServiceModeling.Elements;

namespace MassServiceModeling.NextElement;

public abstract class NextElementsContainer
{
    protected abstract Element GetNextElement();
    public void InAct() => GetNextElement().InAct();
}