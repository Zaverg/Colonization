using System;

public class FlagPlaceState : CollectorBaseState
{
    private ICollectorBase _collectorBase;
    private MiningTask _miningTask;
    private BaseBuildTask _baseBuildTask;

    public override event Action Completed;

    public FlagPlaceState(MiningTask miningTask, BaseBuildTask baseBuildTask)
    {
        _miningTask = miningTask;
        _baseBuildTask = baseBuildTask;
    }

    public override void Entry(ICollectorBase collectorBase)
    {
        _collectorBase = collectorBase;
    }
    public override void Run()
    {
        if (_collectorBase == null)
            return;

        CollectorBot collectorBot = _collectorBase.BotDispatcher.GetAvailableCollectorBot();

        if (collectorBot == null)
            return;

        if (_collectorBase.ResurceCounter.CollectedResources >= _collectorBase.CountResurceToBuildBase)
        {
            _baseBuildTask.AssignTask(collectorBot);

            return;
        }

        _miningTask.AssignTask(collectorBot);
    }

    public override void Exit()
    {
        _collectorBase = null;
    }
}
