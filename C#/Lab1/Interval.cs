namespace Lab1;

public class Interval
{
    public double StartPoint { get; private set; }
    public double EndPoint { get; private set; }
    public int Count { get; private set; }
    private const int IntervalMinCapacity = 5;

    public Interval(double startPoint, double endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }

    public bool IsInInterval(double number) => number >= StartPoint && number < EndPoint;

    public static List<Interval> SplitNumbersIntoEqualIntervals(List<double> numbers, int intervalsCount = 20)
    {
        List<Interval> intervals = new();
        double min = numbers.Min();
        double max = numbers.Max();
        double step = (max - min) / intervalsCount;

        for (int i = 0; i < intervalsCount; i++)
        {
            double intervalStart = i * step + min;
            double intervalEnd = intervalStart + step;

            intervals.Add(CreateAndFillInterval(intervalStart, intervalEnd, numbers));
        }

        intervals[^1].Count++;
        return intervals;
    }

    public static Interval CreateAndFillInterval(double startPoint, double endPoint, List<double> numbers)
    {
        Interval interval = new(startPoint, endPoint);
        foreach (var number in numbers)
        {
            if (interval.IsInInterval(number))
            {
                interval.Count++;
            }
        }

        return interval;
    }

    public static List<Interval> UniteSmallIntervals(List<Interval> intervals)
    {
        List<Interval> unitedIntervals = new List<Interval>(intervals);
        for (int i = 0; i < unitedIntervals.Count; i++)
        {
            if (unitedIntervals[i].Count < IntervalMinCapacity)
            {
                int leftIndex = DefineLeftIndexForMerging(i, unitedIntervals);
                int rightIndex = leftIndex + 1;

                var newInterval = MergeIntervalsLeftWithRight(unitedIntervals[leftIndex], unitedIntervals[rightIndex]);
                unitedIntervals[leftIndex] = newInterval;
                unitedIntervals.RemoveAt(rightIndex);

                i--;
            }
        }

        return unitedIntervals;
    }

    private static int DefineLeftIndexForMerging(int i, List<Interval> intervals)
    {
        int leftIndex = i - 1;
        if (i + 1 < intervals.Count - 1)
        {
            if (i == 0 || intervals[i + 1].Count < intervals[i - 1].Count)
                leftIndex = i;
        }

        return leftIndex;
    }

    public static Interval MergeIntervalsLeftWithRight(Interval left, Interval right)
    {
        Interval newInterval = new Interval(left.StartPoint, right.EndPoint)
        {
            Count = left.Count + right.Count
        };

        return newInterval;
    }
}