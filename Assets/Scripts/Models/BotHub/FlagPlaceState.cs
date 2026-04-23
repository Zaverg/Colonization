using System;

public class FlagPlaceState : BaseState
{
    private IBotHub _collectorBase;
    private MiningTask _miningTask;
    private CollectorBaseTask _mainTask;
    private bool _isBuilderAssigned;

    private CollectorBot _assignedBot;

    public override event Action Completed;

    public FlagPlaceState(MiningTask miningTask)
    {
        _miningTask = miningTask;
    }

    public override void Entry(IBotHub collectorBase)
    {
        _collectorBase = collectorBase;
        _mainTask = _collectorBase.MainTask;
    }

    public override void Run()
    {
        if (_collectorBase.BotDispatcher.AvailableCollectorsCount == 0)
            return;

        CollectorBot collectorBot = _collectorBase.BotDispatcher.GetAvailableBot();

        if (_isBuilderAssigned == false && _collectorBase.ResourceCounter.CollectedResources >= _collectorBase.CountResourceToBuildBase)
        {
            _assignedBot = collectorBot;

            _collectorBase.Flag.Deactivated += _assignedBot.ResetTasks;
            _assignedBot.AssignTasks(_mainTask.CreateTask());
            
            _isBuilderAssigned = true;

            return;
        }

        if (_collectorBase.MineralRegistry.AvailableMineralsCount > 0)
            collectorBot.AssignTasks(_miningTask.CreateTask());
        else
            _collectorBase.BotDispatcher.EnqueueBot(collectorBot);
    }

    public override void Exit()
    {
        if (_assignedBot != null)
        {
            _collectorBase.Flag.Deactivated -= _assignedBot.ResetTasks;
            _assignedBot = null;
        }

        _collectorBase = null;
        _isBuilderAssigned = false;
    }
}
