namespace MassServiceModeling.ItemsQueues;

public class ItemsQueue
{
    public int Limit { get; }
    public int Length => Queue.Count;
    protected List<Item> Queue { get; } = new();
    
    public ItemsQueue(int limit = int.MaxValue)
    {
        Limit = limit;
    }

    public bool TryAdd(Item item)
    {
        if (Length >= Limit) return false;
        Queue.Add(item);
        return true;
    }
    
    public static bool TrySwapLast(ItemsQueue from, ItemsQueue to)
    {
        if (from.Length <= 0 || to.Length >= to.Limit) return false;
        to.Queue.Add(from.Queue[from.Length - 1]);
        from.Queue.RemoveAt(from.Length - 1);
        return true;
    }
    
    public virtual Item GetItem()
    {
        var item = Queue[0];
        Queue.RemoveAt(0);
        return item;
    }
}