using DistributionRandomizer.DelayRandomizers;
using Lab3.Tasks.Task3.Elements;
using MassServiceModeling;
using MassServiceModeling.Elements;
using MassServiceModeling.NextElement;

namespace Lab3.Tasks.Task3;

public class Task3Hospital
{
    public Model Model { get; }
    
    public Task3Hospital()
    {
        CreateClient patients = new CreateClient(new ExponentialRandomizer(15), "Patient");
        DoctorProcess doctors = new(2, "Doctors", "Doctor");
        // Process attendants = new(new UniformRandomizer(3, 8), subProcessCount: 3, name: "Attendants", subProcessName: "Attendant");
        Process attendants = new(new UniformRandomizer(3, 8), name: "Attendants", subProcessName: "Attendant");
        Process fromHospitalToLab = new(new UniformRandomizer(2, 5), 25, name: "WayToLab");
        // Process labRegistry = new(new ErlangRandomizer(4.5, 3), name: "Registry");
        Process labRegistry = new(new ErlangRandomizer(4.5, 3), subProcessCount: 2, name: "Registry");
        LabAssistanceProcess labAssistants = new(new ErlangRandomizer(4, 2), assistanceCount: 2, "Assistants", subProcessName: "Assistant");
        Process fromLabToHospital = new(new UniformRandomizer(2, 5), 25, name: "WayToHospital");

        patients.NextElementsContainer = new NextElementContainer(doctors);
        doctors.NextElementsContainer = new NextAfterDoctor(doctors, attendants, fromHospitalToLab);
        fromHospitalToLab.NextElementsContainer = new NextElementContainer(labRegistry);
        labRegistry.NextElementsContainer = new NextElementContainer(labAssistants);
        labAssistants.NextElementsContainer = new NextElementContainer(fromLabToHospital);
        fromLabToHospital.NextElementsContainer = new NextElementContainer(doctors);

        Model = new Task3HospitalModel(new List<Element>() { patients, doctors, attendants, fromHospitalToLab, labRegistry, labAssistants, fromLabToHospital });
    }
}