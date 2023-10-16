namespace DistributionRandomizer.DelayRandomizers;

public abstract class Randomizer
{
    public abstract double GenerateDelay();

    public List<double> GenerateListOfDelays(int size)
    {
        List<double> numbers = new();
        for (int i = 0; i < size; i++)
        {
            numbers.Add(GenerateDelay());
        }

        return numbers;
    }
}