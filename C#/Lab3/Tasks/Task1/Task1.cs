using MassServiceModeling;
using MassServiceModeling.Elements;
using MassServiceModeling.NextElement;

namespace Lab3.Tasks.Task1;

public class Task1
{
    public Model Model;

    private Create _create = new();
    private Process _process1 = new(5, 2, maxQueue: 5);
    private Process _process2 = new(7, 3, maxQueue: 5);
    private Process _process3 = new(10, 4, maxQueue: 5);
    
    public Task1()
    {
        var container = new NextElementsContainerByProbability();
        container.AddNextElement(_process1, 40);
        container.AddNextElement(_process2, 50);
        container.AddNextElement(_process3, 10);
        _create.NextElementsContainer = container;
        
        Model = new Model(new List<Element> { _create, _process1, _process2, _process3 });
    }
}