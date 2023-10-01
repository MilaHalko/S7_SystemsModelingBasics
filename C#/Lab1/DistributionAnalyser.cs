using Lab1.Generators;

namespace Lab1;

public class DistributionAnalyser
{
    private double[] _numbers;
    private IGenerator _generator;
    
    public DistributionAnalyser(IGenerator generator, int numbersCount = 1000)
    {
        _generator = generator;
        _numbers = _generator.Generate(numbersCount);
    }

    public void Run()
    {
        _numbers = _generator.Generate(_numbers.Length);

        foreach (var number in _numbers)
        {
            Console.Out.Write($"{number} ");
        }
    }
}