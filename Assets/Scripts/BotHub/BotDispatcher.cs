using System;
using System.Collections.Generic;
using UnityEngine;

public class BotDispatcher : MonoBehaviour
{
    private List<CollectorBot> _allBots;
    private Queue<CollectorBot> _availableBots;
    private ResourceCounter _resourceCounter;

    public event Action<int> CountChanged;

    public int AvailableBotsCount => _availableBots.Count;
    public int AllBotsCount => _allBots.Count;

    public void Initialize(ResourceCounter resourceCounter)
    {
        _allBots = new List<CollectorBot>();
        _availableBots = new Queue<CollectorBot>();
        _resourceCounter = resourceCounter;
    }

    public CollectorBot GetAvailableBot()
    {
        CollectorBot collectorBot = _availableBots.Dequeue();
        SubscribeToBot(collectorBot);

        return collectorBot;
    }

    public void EnqueueBot(CollectorBot bot)
    {
        UnsubscribeToBot(bot);
        
        _availableBots.Enqueue(bot);

        if (_allBots.Contains(bot) == false)
        {
            _allBots.Add(bot);

            CountChanged?.Invoke(_allBots.Count);
        }
    }

    public void UnregisterBot(CollectorBot bot)
    {
        _allBots.Remove(bot);
        UnsubscribeToBot(bot);

        CountChanged?.Invoke(_allBots.Count);
    }

    private void SubscribeToBot(CollectorBot bot)
    {
        bot.BotFreed += EnqueueBot;
        bot.Unloader.Unloaded += _resourceCounter.UpdateCounter;
    }

    private void UnsubscribeToBot(CollectorBot bot)
    {
        bot.BotFreed -= EnqueueBot;
        bot.Unloader.Unloaded -= _resourceCounter.UpdateCounter;
    }
}