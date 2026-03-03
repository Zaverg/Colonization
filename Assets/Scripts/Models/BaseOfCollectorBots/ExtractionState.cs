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
        if (_collectorBase == null)
            return;

        if (_collectorBase.ResurceCounter.CollectedResources >= _collectorBase.CountResurceToCreateBot)
            _collectorBase.BotDispatcher.EnqueueCollector(_collectorBotFactory.Create());

        CollectorBot collectorBot = _collectorBase.BotDispatcher.GetAvailableCollectorBot();

        if (collectorBot != null)
            _miningTask.AssignTask(collectorBot);
    }

    public override void Exit()
    {
        _collectorBase = null;
    }
}