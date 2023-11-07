using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Elements;
using MassServiceModeling.Items;
using MassServiceModeling.NextElement;

namespace Lab3.Tasks.Task2;

public class Task2CarBank
{
    public Task2Model Model;

    private Create _cars = new(0.5, name: "Cars");
    private Process _cashier1 = new(new NormalRandomizer(1, 0.3), maxQueue: 3, name: "Cashier1");
    private Process _cashier2 = new(new NormalRandomizer(1, 0.3), maxQueue: 3, name: "Cashier2");

    public Task2CarBank()
    {
        // Connecting elements
        var cashiers = new NextElementsContainerByQueuePriority();
        cashiers.AddNextElement(_cashier1, 1);
        cashiers.AddNextElement(_cashier2, 2);
        _cars.NextElementsContainer = cashiers;

        // Changing queues condition
        _cashier1.OnQueueChanged += ChangeQueueIfNecessary;
        _cashier2.OnQueueChanged += ChangeQueueIfNecessary;

        // Initial states condition
        const double startTime = 0.1;
        _cars.NextT = startTime;
        for (var i = 0; i < 3; i++)
        {
            _cashier1.InAct(new Item(startTime));
            _cashier2.InAct(new Item(startTime));
        }

        Model = new Task2Model(new List<Element> { _cars, _cashier1, _cashier2 });
    }

    private void ChangeQueueIfNecessary()
    {
        if (Math.Abs(_cashier1.Queue.Length - _cashier2.Queue.Length) < 2) return;
        Console.WriteLine($"\nCHANGE_QUEUE: Q1={_cashier1.Queue.Length} Q2={_cashier2.Queue.Length}!*!*!*!*!");
        if (_cashier1.Queue.Length > _cashier2.Queue.Length)
        {
            Process.TryChangeQueueForLastItem(_cashier1, _cashier2);
            Console.WriteLine("CASHIER1 -> CASHIER2");
        }
        else
        {
            Process.TryChangeQueueForLastItem(_cashier2, _cashier1);
            Console.WriteLine("CASHIER2 -> CASHIER1");
        }

        Model.QueueChangeCount7++;
    }
}