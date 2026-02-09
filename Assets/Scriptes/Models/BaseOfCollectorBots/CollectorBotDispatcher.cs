using UnityEngine;
using System.Collections.Generic;

public class CollectorBotDispatcher : MonoBehaviour
{
    private Queue<CollectorBot> _availableCollectors;

    public int AvailableCollectorsCount => _availableCollectors.Count;

    public void Initialize(List<CollectorBot> collectorBots)
    {
        _availableCollectors = new Queue<CollectorBot>(collectorBots);
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