namespace DistributionRandomizer.NumberRandomizers;

public abstract class Randomizer
{
    public abstract double GenerateNumber();

    public double GenerateDouble() => new Random().NextDouble();
    public List<double> GenerateListOfNumbers(int size)
    {
        List<double> numbers = new();
        for (int i = 0; i < size; i++)
        {
            numbers.Add(GenerateNumber());
        }

        return numbers;
    }
}