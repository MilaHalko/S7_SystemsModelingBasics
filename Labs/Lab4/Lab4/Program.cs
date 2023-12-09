using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MassServiceModeling.Model;

BenchmarkRunner.Run<Lab4Benchmark>();

public class Lab4Benchmark
{
    [Params(1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000)]
    public int ModelTime;
    private Model? _sequentialModel;
    private Model? _branchModel;
    
    [IterationSetup]
    public void CreateModel()
    {
        _sequentialModel = ModelHelper.CreateSequential(50);
        _branchModel = ModelHelper.CreateBranching2Levels(10, 5);
    }

    [Benchmark]
    public void SimulateSequential() => _sequentialModel.Simulate(time: ModelTime, printResult: false);
    [Benchmark]
    public void SimulateBranching() => _branchModel.Simulate(time: ModelTime, printResult: false);
}