using System.Collections.Generic;

public class CollectorBotDispatcher
{
    private Queue<CollectorBot> _availableCollectors;

    public int AvailableCollectorsCount => _availableCollectors.Count;

    public CollectorBotDispatcher()
    {
        _availableCollectors = new Queue<CollectorBot>();
    }

    public CollectorBot GetAvailableCollectorBot()
    {
        return _availableCollectors.Dequeue();
    }

    public void EnqueueCollector(CollectorBot collectorBot)
    {
        _availableCollectors.Enqueue(collectorBot);
    }
}