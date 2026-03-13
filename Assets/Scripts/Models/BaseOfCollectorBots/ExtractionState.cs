using System;

public class ExtractionState : CollectorBaseState
{
    private ICollectorBase _collectorBase;
    private MiningTask _miningTask;

    public override event Action Completed;

    public ExtractionState(MiningTask miningTask)
    {
        _miningTask = miningTask;
    }

    public override void Entry(ICollectorBase collectorBase)
    {
        _collectorBase = collectorBase;
    }

    public override void Run()
    {
        if (_collectorBase.ResourceCounter.CollectedResources >= _collectorBase.CountResourceToCreateBot)
        {
            CollectorBot newBot = _collectorBase.FactoryBot.Create(_collectorBase.SpawnBotPlace.position, true) as CollectorBot;
            _collectorBase.ResourceCounter.SubtractCounter(_collectorBase.CountResourceToCreateBot);
            _collectorBase.BotDispatcher.EnqueueBot(newBot);
        }
        
        if (_collectorBase.BotDispatcher.AvailableCollectorsCount <= 0 || _collectorBase.MineralRegistry.AvailableMineralsCount == 0)
            return;

        CollectorBot collectorBot = _collectorBase.BotDispatcher.GetAvailableBot();
        collectorBot.AssignTasks(_miningTask.CreateTask());
    }

    public override void Exit()
    {
        _collectorBase = null;
    }
}