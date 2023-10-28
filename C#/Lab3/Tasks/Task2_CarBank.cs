using MassServiceModeling;
using MassServiceModeling.Elements;
using MassServiceModeling.NextElement;

namespace Lab3;

public class Task2_CarBank : Task
{
    public Task2_CarBank()
    {
        // TODO: Statistics condition :  2 cashiers, 2 queues for 2 lanes
        // TODO: Create interface for NExtElement
        // TODO: Implement for special case
        // TODO: Event for changing queue, add logic here
        Create create = new Create(0.5);
        Process process1 = new Process(delay: 0.3, maxQueue: 3);
        Process process2 = new Process(delay: 0.3, maxQueue: 3);
        
        process1.InAct();
        process2.InAct();
        
        create.NextT = 0.1;

        // TODO: Priority by queue :     First queue if <= Second queue
        // TODO: Move to other queue :   Last && Queue1-Queue2 >= 2
        var container = new NextElementsContainerByProbability();
        container.AddNextElement(process1, 40);
        container.AddNextElement(process2, 60);
        create._nextElementsContainer = container;

        model = new(new List<Element>() { create, process1, process2 });
    }
}