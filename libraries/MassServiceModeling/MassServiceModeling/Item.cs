namespace MassServiceModeling;

public class Item
{
    private static int _id; 
    public int Id { get; } = _id++;
    public string Name { get; set;  } = "";
    public double StartTime { get; }
    
    public Item(double startTime)
    {
        StartTime = startTime;
    }
}