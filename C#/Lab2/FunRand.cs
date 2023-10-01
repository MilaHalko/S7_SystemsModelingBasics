namespace Lab2;

public static class FunRand
{
    private static double GetRandom()
    {
        double a = 0;
        while (a == 0) { a = new Random().NextDouble(); }
        return a;
    }
    
    public static double Exponential(double timeMean)
    {
        return -timeMean * Math.Log(GetRandom());
    }

    public static double Uniform(double timeMin, double timeMax)
    {
        return timeMin + GetRandom() * (timeMax - timeMin);
    }

    public static double Normal(double timeMean, double timeDeviation)
    {
        double u1 = 1.0 - new Random().NextDouble();
        double u2 = 1.0 - new Random().NextDouble();

        // Box-Muller
        double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        return timeMean + timeDeviation * z;
    }
}