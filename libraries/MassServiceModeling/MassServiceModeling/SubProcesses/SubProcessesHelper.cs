using MassServiceModeling.Elements;

namespace MassServiceModeling.SubProcesses;

public class SubProcessesHelper
{
    public int Count => Container.Count;
    public int WorkingCount => Container.Count(s => s.IsWorking);
    
    public List<SubProcess> All => Container;
    public List<SubProcess> ForOutAct => Container.Where(s => s.Time.Next <= _parentProcess.Time.Curr && s.IsWorking).ToList();
    public SubProcess Free => Container.First(s => !s.IsWorking);

    protected List<SubProcess> Container { get; } = new();
    private Process _parentProcess;
    
    public SubProcessesHelper(Process process) => _parentProcess = process;
    public void Add(SubProcess subProcess) => Container.Add(subProcess);
}