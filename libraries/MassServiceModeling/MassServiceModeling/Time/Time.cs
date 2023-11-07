namespace MassServiceModeling.Time;

public class Time
{
    public double All => Curr - Start;
    public double Delta => Next - Curr;
    public double Next;
    public double Curr;
    public double Start;
    public double Delay;

    public void ShiftTime() => Curr = Next;
}