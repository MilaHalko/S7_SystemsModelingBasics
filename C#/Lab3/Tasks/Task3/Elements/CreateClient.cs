using DistributionRandomizer.DelayRandomizers;
using Lab3.Tasks.Task3.Clients;
using MassServiceModeling;
using MassServiceModeling.Elements;

namespace Lab3.Tasks.Task3.Elements;

public class CreateClient : Create
{
    public CreateClient(Randomizer randomizer, string name) : base(randomizer, name) {}

    protected override Item CreateItem()
    {
        var number = new Random().NextDouble();
        if (number <= 0.5) return new Client("ChamberClient", ClientType.Chamber, 15, CurrT);
        if (number <= 0.6) return new Client("NotExaminedChamberClient", ClientType.NotExamined, 40, CurrT);
        return new Client("LabClient", ClientType.Lab, 30, CurrT);
    }
}