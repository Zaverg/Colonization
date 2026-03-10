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

    public CollectorBot GetAvailableBot()
    {
        CollectorBot collectorBot = _availableCollectors.Dequeue();
        SubscribeToBot(collectorBot);

        return collectorBot;
    }

    public void EnqueueBot(CollectorBot bot)
    {
        UnSubscribeToBot(bot);
        
        _availableCollectors.Enqueue(bot);
    }

    public void FreeBot(CollectorBot bot)
    {
        UnSubscribeToBot(bot);
    }

    private void SubscribeToBot(CollectorBot bot)
    {
        bot.OnBotAvailable += EnqueueBot;
        bot.Unloader.Unloaded += _resourceCounter.UpdateCounter;
    }

    private void UnSubscribeToBot(CollectorBot bot)
    {
        bot.OnBotAvailable -= EnqueueBot;
        bot.Unloader.Unloaded -= _resourceCounter.UpdateCounter;
    }
}