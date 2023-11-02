using MassServiceModeling;
using MassServiceModeling.Elements;
using MassServiceModeling.NextElement;

namespace Lab3.Tasks;

public class Task1
{
    public Model Model;
    
    Create _create = new Create(1);
    Process _process1 = new Process(1, 2, maxQueue: 5);
    Process _process2 = new Process(2, 3, maxQueue: 5);
    Process _process3 = new Process(1, 4, maxQueue: 5);
    
    public Task1()
    {
        var container = new NextElementsContainerByProbability();
        container.AddNextElement(_process1, 40);
        container.AddNextElement(_process2, 50);
        container.AddNextElement(_process3, 10);
        _create.NextElementsContainer = container;
        
        Model = new(new List<Element>() { _create, _process1, _process2, _process3 });
    }
}