namespace Lab1.Generators;

public abstract class Generator
{
    
    protected abstract double GenerateNumber();

    public List<double> Generate(int size)
    {
        List<double> numbers = new();
        for (int i = 0; i < size; i++)
        {
            numbers.Add( GenerateNumber());
        }
        return numbers;
    }

    public abstract Func<double, double, double> GetIntegralFunc();
}