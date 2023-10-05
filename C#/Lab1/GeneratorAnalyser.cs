using System.Globalization;
using System.Text;
using Lab1.Generators;
using static Lab1.ChiCriticalValuesHelper;

namespace Lab1;

public class GeneratorAnalyser
{
    private List<double> _numbers = new();
    private List<Interval> _intervals = new();
    private List<Interval> _unitedIntervals = new();
    private double chi;
    private double tableChi;

    public int NumbersCount { get; set; } = 1000;
    public int IntervalsCount { get; set; } = 20;

    public bool GetChiIsOkAfterAnalysis(Generator generator)
    {
        _numbers = generator.Generate(NumbersCount);
        _intervals = Interval.SplitNumbersIntoEqualIntervals(_numbers, IntervalsCount);
        _unitedIntervals = Interval.UniteSmallIntervals(_intervals);
        chi = CalculateChiSquared(_unitedIntervals, generator.GetIntegralFunc());
        tableChi = GetChiCriticalValue(_unitedIntervals.Count - 2);
        return chi < tableChi;
    }

    public double GetChiAfterAnalysisWithPrint(Generator generator)
    {
        bool chiIsOk = GetChiIsOkAfterAnalysis(generator);
        PrintIntervalsAsColumn(_intervals);
        Console.Out.WriteLine($"Intervals count: {_intervals.Count} -> {_unitedIntervals.Count}");
        Console.Out.WriteLine($"Average: {GetAverage()}");
        Console.Out.WriteLine($"Dispersion: {GetDispersion()}");
        Console.Out.WriteLine($"Chi: {chi:F3} {(chiIsOk ? "<" : ">= ")} {tableChi} {(chiIsOk ? "OK" : "NOT OK")}");
        return chi;
    }
    
    public void TestChiIsOkPercent(Generator generator, int TESTS_COUNT = 1000)
    {
        int chiIsOk = Enumerable.Range(0, TESTS_COUNT).Select(_ => GetChiIsOkAfterAnalysis(generator) ? 1 : 0).Sum();
        double goodChiPercent = (double)chiIsOk / TESTS_COUNT * 100;
        Console.Out.WriteLine($"Percent of good chi: {goodChiPercent:F2}%");
    }

    private double GetAverage() => _numbers.Average();

    private double GetDispersion()
    {
        double average = GetAverage();
        double dispersion = 0;
        foreach (var number in _numbers) dispersion += Math.Pow(number - average, 2);

        return dispersion / (_numbers.Count - 1);
    }

    private double CalculateChiSquared(List<Interval> intervals, Func<double, double, double> integralFunc)
    {
        double chi = 0;
        foreach (var interval in intervals)
        {
            double npi = GetNumbersCountInAllIntervals(intervals) *
                         integralFunc(interval.StartPoint, interval.EndPoint);
            chi += Math.Pow((interval.Count - npi), 2) / npi;
        }

        return chi;
    }

    private int GetNumbersCountInAllIntervals(List<Interval> intervals) =>
        Enumerable.Sum(intervals, interval => interval.Count);

    private void PrintIntervalsAsColumn(List<Interval> intervals)
    {
        CultureInfo customCulture = new CultureInfo("fr-FR") { NumberFormat = { NumberDecimalSeparator = "," } };

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
            string format = "+0.00;-0.00;0.000";
            sb.Append(
                $"[{interval.StartPoint.ToString(format)}; {interval.EndPoint.ToString(format)}) {interval.Count} ");
            for (int i = 0; i < interval.Count; i++)
            {
                // sb.Append(".");
            }

            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString());
    }
}