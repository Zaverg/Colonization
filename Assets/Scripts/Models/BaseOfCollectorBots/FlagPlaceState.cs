using System;
using System.Diagnostics;
using Unity.Android.Types;

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
        Debug.Print("FlagPlaceState");

        if (_collectorBase == null)
            return;

        CollectorBot collectorBot = _collectorBase.BotDispatcher.GetAvailableCollectorBot();

        if (collectorBot == null)
            return;

        if (_collectorBase.ResourceCounter.CollectedResources >= _collectorBase.CountResourceToBuildBase)
        {
           collectorBot.AssignTasks(_baseBuildTask.CreateTask());

            return;
        }

        collectorBot.AssignTasks(_miningTask.CreateTask());
    }

    public override void Exit()
    {
        _collectorBase = null;
    }
}
