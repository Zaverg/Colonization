using System;

public class ExtractionState : CollectorBaseState
{
    private ICollectorBase _collectorBase;
    private MiningTask _miningTask;
    private CollectorBotFactory _collectorBotFactory;

    public override event Action Completed;

    public ExtractionState(MiningTask miningTask, CollectorBotFactory collectorBotFactory)
    {
        _miningTask = miningTask;
        _collectorBotFactory = collectorBotFactory;
    }

    public override void Entry(ICollectorBase collectorBase)
    {
        _collectorBase = collectorBase;
    }

    public override void Run()
    {
        if (_collectorBase.BotDispatcher.AvailableCollectorsCount <= 0 || _collectorBase.MineralRegistry.AvailableMineralsCount == 0)
            return;

        CollectorBot collectorBot = _collectorBase.BotDispatcher.GetAvailableCollectorBot();
        collectorBot.AssignTasks(_miningTask.CreateTask());
    }

    public override void Exit()
    {
        _collectorBase = null;
    }
}