namespace Lab1.Generators;

public class NormalGenerator : IGenerator
{
    private readonly Random _random = new Random();
    private readonly double _sigma;
    private readonly double _a;
    
    public NormalGenerator(double sigma = 1, double a = 0)
    {
        _sigma = sigma;
        _a = a;
    }

    private double GenerateNumber()
    {
        double U = Enumerable.Range(0, 12).Select(_ => _random.NextDouble()).Sum() - 6;
        return _sigma * U + _a;
    }

    public double[] Generate(int size)
    {
        double[] numbers = new Double[size];
        for (int i = 0; i < size; i++)
        {
            numbers[i] = GenerateNumber();
        }
        return numbers;
    }
}