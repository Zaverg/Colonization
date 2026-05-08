using System;

public class DefaultState : BaseState
{
    private IBotHub _botHub;
    private MiningTask _miningTask;
    private CollectorBotFactory _collectorBotFactory;

    public override event Action<Type> Completed;

    public DefaultState(MiningTask miningTask, CollectorBotFactory collectorBotSpawner)
    {
        _miningTask = miningTask;
        _collectorBotFactory = collectorBotSpawner;
    }

    public override void Entry(IBotHub collectorBase)
    {
        _botHub = collectorBase;
    }

    public override void Run()
    {
        if (_botHub.ResourceCounter.CollectedResources >= _botHub.PriceList.CountResourceToBuildBotHub && _botHub.Flag.gameObject.activeSelf)
        {
            Completed?.Invoke(typeof(FlagPlaceState));
        }
        else if (_botHub.ResourceCounter.CollectedResources >= _botHub.PriceList.CountResourceToCreateBot)
        {
            UnityEngine.Debug.Log(_collectorBotFactory);
            CollectorBot newBot = _collectorBotFactory.Create(_botHub.SpawnBotPlace.position);
            _botHub.ResourceCounter.Subtract(_botHub.PriceList.CountResourceToCreateBot);
            _botHub.BotDispatcher.EnqueueBot(newBot);
        }

        if (_botHub.BotDispatcher.AvailableBotsCount != 0 && _botHub.MineralRegistry.AvailableMineralsCount != 0)
        {
            CollectorBot collectorBot = _botHub.BotDispatcher.GetAvailableBot();
            collectorBot.AssignTasks(_miningTask.CreateTask());
        }
    }

    public override void Exit()
    {
        _botHub = null;
    }
}