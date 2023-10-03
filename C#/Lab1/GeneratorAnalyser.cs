using System.Globalization;
using System.Text;
using Lab1.Generators;
using static Lab1.ChiCriticalValuesHelper;

namespace Lab1;

public class GeneratorAnalyser
{
    private List<double> _numbers = new();
    private List<Interval> intervals = new();
    private List<Interval> unitedIntervals = new();
    
    public int NumbersCount { get; set; } = 1000;
    public int IntervalsCount { get; set; } = 20;

    public void RunFullAnalysis(Generator generator)
    {
        _numbers = generator.Generate(NumbersCount);
        intervals = Interval.SplitNumbersIntoEqualIntervals(_numbers, IntervalsCount);
        PrintIntervalsInOneLine(intervals);

        unitedIntervals = Interval.UniteSmallIntervals(intervals);
        // Console.Out.WriteLine("United version:");
        // PrintIntervals(unitedIntervals);

        PrintAllIntervalsCount();
        var chiSquared = ChiFullChecking(unitedIntervals, generator.GetIntegralFunc());
    }

    private double ChiFullChecking(List<Interval> unitedIntervals, Func<double, double, double> integralFunc)
    {
        Generator generator;
        double chiSquared = CalculateChiSquared(unitedIntervals, integralFunc);
        int v = unitedIntervals.Count - 2;
        bool chiIsOk = CheckChiSquared(chiSquared, v);
        Console.Out.WriteLine($"Chi-squared is {(chiIsOk ? "OK" : "NOT OK")}");
        Console.Out.WriteLine($"{chiSquared:F3} {(chiIsOk ? "<" : ">= ")} {GetChiCriticalValue(v)}\n");
        return chiSquared;
    }

    public double CalculateChiSquared(List<Interval> intervals, Func<double, double, double> integralFunc)
    {
        double chi = 0;
        foreach (var interval in intervals)
        {
            double npi = GetNumbersCountInAllIntervals(intervals) * integralFunc(interval.StartPoint, interval.EndPoint);
            chi += Math.Pow((interval.Count - npi), 2) / npi;
        }

        return chi;
    }

    private int GetNumbersCountInAllIntervals(List<Interval> intervals)
    {
        int count = 0;
        foreach (var interval in intervals)
        {
            count += interval.Count;
        }

        return count;
    }

    public void PrintIntervalsInOneLine(List<Interval> intervals)
    {
        CultureInfo customCulture = new CultureInfo("fr-FR");
        customCulture.NumberFormat.NumberDecimalSeparator = ",";

        foreach (var interval in intervals)
        {
            Console.Out.WriteLine($"{interval.StartPoint.ToString(customCulture)}");
        }

        foreach (var interval in intervals)
        {
            Console.Out.Write($"{interval.Count}\n");
        }

        Console.Out.WriteLine("");
    }

    public void PrintIntervals(List<Interval> intervals)
    {
        StringBuilder sb = new();
        foreach (var interval in intervals)
        {
            string format = "+0,00;-0,00;0,000";
            sb.Append(
                $"[{interval.StartPoint.ToString(format)}; {interval.EndPoint.ToString(format)}) {interval.Count:D3}: ");
            for (int i = 0; i < interval.Count; i++)
            {
                sb.Append(".");
            }

            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString());
    }

    private void PrintAllIntervalsCount()
    {
        Console.Out.WriteLine($"Initial count: {intervals.Count} \n" +
                              $"United  count: {unitedIntervals.Count}");
    }
}