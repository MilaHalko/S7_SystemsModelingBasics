using MassServiceModeling.TimeClasses;

namespace MassServiceModeling.SubProcesses;

public class SubProcessesContainer
{
    public int Count => Container.Count;
    public int WorkingCount => Container.Count(s => s.IsWorking);
    
    public List<SubProcess> All => Container;
    public List<SubProcess> ForOutAct => Container.Where(s => s.NextT <= Time.Curr && s.IsWorking).ToList();
    public SubProcess Free => Container.First(s => !s.IsWorking);

    protected List<SubProcess> Container { get; } = new();
    
    public void Add(SubProcess subProcess) => Container.Add(subProcess);
}