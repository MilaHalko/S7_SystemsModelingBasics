using MassServiceModeling.SubProcesses;

namespace MassServiceModeling.Statistics;

public class SubProcessStatisticHelper
{
    public int Quantity { get; set; }
    public double WorkTime { get; set; }
    
    private SubProcess _subProcess;
    
    public SubProcessStatisticHelper(SubProcess subProcess) => _subProcess = subProcess;
}