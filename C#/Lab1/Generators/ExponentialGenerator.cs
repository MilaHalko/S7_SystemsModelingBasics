namespace Lab1.Generators;

public class ExponentialGenerator : IGenerator
{
    private readonly double _lambda;
    private readonly Random _random = new Random();

    public ExponentialGenerator(double lambda = 1)
    {
        _lambda = lambda;
    }

    public double[] Generate(int size)
    {
        double[] numbers = new Double[size];
        for (int i = 0; i < size; i++)
        {
            numbers[i] = (-1 / _lambda) * Math.Log(_random.NextDouble());
        }
        return numbers;
    }
}