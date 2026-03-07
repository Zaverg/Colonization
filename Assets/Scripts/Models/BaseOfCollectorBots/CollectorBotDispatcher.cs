using System.Collections.Generic;

public class CollectorBotDispatcher
{
    private Queue<CollectorBot> _availableCollectors;
    private ResourceCounter _resourceCounter;

    public int AvailableCollectorsCount => _availableCollectors.Count;

    public CollectorBotDispatcher(ResourceCounter resourceCounter)
    {
        _availableCollectors = new Queue<CollectorBot>();
        _resourceCounter = resourceCounter;
    }

    public CollectorBot GetAvailableCollectorBot()
    {
        CollectorBot collectorBot = _availableCollectors.Dequeue();
        SubscribeToBot(collectorBot);

        return collectorBot;
    }

    public void EnqueueCollector(CollectorBot bot)
    {
        UnSubscribeToBot(bot);
        
        _availableCollectors.Enqueue(bot);
    }

    private void SubscribeToBot(CollectorBot bot)
    {
        bot.OnBotAvailable += EnqueueCollector;
        bot.Unloader.Unloaded += _resourceCounter.UpdateCounter;
    }

    private void UnSubscribeToBot(CollectorBot bot)
    {
        bot.OnBotAvailable -= EnqueueCollector;
        bot.Unloader.Unloaded -= _resourceCounter.UpdateCounter;
    }
}