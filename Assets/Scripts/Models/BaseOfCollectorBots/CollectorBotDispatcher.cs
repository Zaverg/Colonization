using System.Collections.Generic;

public class CollectorBotDispatcher
{
    private List<CollectorBot> _allCollectors;
    private Queue<CollectorBot> _availableCollectors;
    private ResourceCounter _resourceCounter;

    public int AvailableCollectorsCount => _availableCollectors.Count;
    public int AllCollectorsCount => _allCollectors.Count;

    public CollectorBotDispatcher(ResourceCounter resourceCounter)
    {
        _allCollectors = new List<CollectorBot>();
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

        if (_allCollectors.Contains(bot) == false)
            _allCollectors.Add(bot);
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