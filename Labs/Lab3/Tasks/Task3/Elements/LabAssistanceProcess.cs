using DistributionRandomizer.DelayRandomizers;
using Lab3.Tasks.Task3.Clients;
using MassServiceModeling.Elements;

namespace Lab3.Tasks.Task3.Elements;

public class LabAssistanceProcess : Process
{
    public LabAssistanceProcess(Randomizer randomizer, int assistanceCount, string name, string subProcessName, int maxQueue = 2147483647) :
        base(randomizer, assistanceCount, name, maxQueue, subProcessName) {}

    protected override void NextElementsContainerSetup()
    {
        if (Item is Client { ClientType: ClientType.NotExamined } client)
        {
            client.ClientType = ClientType.Chamber;
            client.RegistrationTime = 15;
            client.Name = "ChamberClient";
        }
        else NextElementsContainer = null;
    }
}