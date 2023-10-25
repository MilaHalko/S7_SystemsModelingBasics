using MassServiceModeling;
using MassServiceModeling.Elements;

namespace Lab3;

public class Task2_CarBank : Task
{
    public Task2_CarBank()
    {
        // ASK: math expectation 0.5 or 1.0 for cashier?
        // ASK: sense of "прибуття першого клієнта заплановано на момент часу 0,1 од. часу (початкова умова)"
        // TODO: Statistics condition :  2 cashiers, 2 queues for 2 lanes
        // TODO: Math expectation :      0.5 for Create and 0.3 for Process
        // TODO: Standard deviation :    0.3 for Process
        Create create = new Create(1);
        Process process1 = new Process(1, 2, maxQueue: 3);
        Process process2 = new Process(1, 2, maxQueue: 3);

        // TODO: Priority by queue :     First queue if <= Second queue
        // TODO: Move to other queue :   Last && Queue1-Queue2 >= 2
        create.SetNextElement(process1, 40);
        create.SetNextElement(process2, 50);

        model = new(new List<Element>() { create, process1, process2 });
    }
}