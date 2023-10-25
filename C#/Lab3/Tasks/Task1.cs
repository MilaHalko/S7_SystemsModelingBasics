using MassServiceModeling;
using MassServiceModeling.Elements;

namespace Lab3;

public class Task1 : Task
{
    public Task1()
    {
        Create create = new Create(1);
        Process process1 = new Process(1, 2, maxQueue: 5);
        Process process2 = new Process(2, 3, maxQueue: 5);
        Process process3 = new Process(1, 4, maxQueue: 5);

        create.SetNextElement(process1, 40);
        create.SetNextElement(process2, 50);
        create.SetNextElement(process3, 10);

        model = new(new List<Element>() { create, process1, process2, process3 });
    }
}