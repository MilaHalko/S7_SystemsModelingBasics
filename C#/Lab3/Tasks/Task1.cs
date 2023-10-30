using MassServiceModeling;
using MassServiceModeling.Elements;
using MassServiceModeling.NextElement;

namespace Lab3;

public class Task1 : Task
{
    
    Create create = new Create(1);
    Process process1 = new Process(1, 2, maxQueue: 5);
    Process process2 = new Process(2, 3, maxQueue: 5);
    Process process3 = new Process(1, 4, maxQueue: 5);
    
    public Task1()
    {
        var container = new NextElementsContainerByProbability();
        container.AddNextElement(process1, 40);
        container.AddNextElement(process2, 50);
        container.AddNextElement(process3, 10);
        create.NextElementsContainer = container;
        
        model = new(new List<Element>() { create, process1, process2, process3 });
    }
}