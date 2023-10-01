namespace Lab1.Generators;

public class UniformGenerator : IGenerator
{
    private readonly uint _a; // 5^13 == 1220703125
    private readonly uint _c; // 2^31 == 2147483648
    private uint _z;

    public UniformGenerator(uint a = 1220703125, uint c = 2147483648)
    {
        _a = a;
        _c = c;
        _z = (uint)new Random().Next();
    }

    private double GenerateNumber()
    {
        _z = _a * _z % _c;
        return _z / _c;
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