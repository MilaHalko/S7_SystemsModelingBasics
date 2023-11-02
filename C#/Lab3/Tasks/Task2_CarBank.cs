using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Elements;
using MassServiceModeling.NextElement;

namespace Lab3.Tasks;

public class Task2_CarBank
{
    public Task2Model Model;
    
    Create _cars = new Create(0.5, name: "Cars");
    Process _cashier1 = new Process(new NormalRandomizer(1, 0.3), maxQueue: 3, name: "Cashier1");
    Process _cashier2 = new Process(new NormalRandomizer(1, 0.3), maxQueue: 3, name: "Cashier2");

    public Task2_CarBank()
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
        _cars.NextT = 0.1;
        for (int i = 0; i < 3; i++)
        {
            _cashier1.InAct();
            _cashier2.InAct();
        }

        Model = new Task2Model(new List<Element>() { _cars, _cashier1, _cashier2 });
    }

    private void ChangeQueueIfNecessary()
    {
        if (Math.Abs(_cashier1.Queue - _cashier2.Queue) >= 2)
        {
            Console.WriteLine("DIFFERENCE IS 2 OR MORE!*!*!*!*!");
            if (_cashier1.Queue > _cashier2.Queue)
            {
                _cashier1.Queue--;
                _cashier2.Queue++;
                Console.WriteLine("CASHIER1 -> CASHIER2");
            }
            else
            {
                _cashier2.Queue--;
                _cashier1.Queue++;
                Console.WriteLine("CASHIER2 -> CASHIER1");
            }
            Model.QueueChangeCount_7++;
        }
    }
}