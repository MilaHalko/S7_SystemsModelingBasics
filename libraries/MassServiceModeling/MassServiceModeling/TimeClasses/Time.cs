namespace MassServiceModeling.TimeClasses;

public static class Time
{
    public static double Curr;
    public static double Start { get; private set; }
    public static double All => Curr - Start;

    public static void ShiftCurr(double next) => Curr = next;
    public static double Delta(double next) => next - Curr;
    public static void SetStart(double start) => Curr = Start = start;
}