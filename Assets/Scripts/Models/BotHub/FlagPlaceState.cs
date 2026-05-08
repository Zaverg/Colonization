using System;

public class FlagPlaceState : BaseState
{
    private IBotHub _botHub;
    private BuildTask _buildingTask;
    private MiningTask _miningTask;

    private CollectorBot _assignedBotToBuild;

    public override event Action<Type> Completed;

    public FlagPlaceState(BuildTask buildingTask, MiningTask miningTask)
    {
        _buildingTask = buildingTask;
        _miningTask = miningTask;
    }

    public override void Entry(IBotHub botHub)
    {
        _botHub = botHub;

        if (_botHub.BotDispatcher.AllBotsCount <= 1)
        {
            _botHub.Flag.Deactivate();

            Completed?.Invoke(typeof(DefaultState));
        }
    }

    public override void Run()
    {
        if (_botHub.BotDispatcher.AvailableBotsCount == 0)
            return;
        
        if (_assignedBotToBuild == null && _botHub.ResourceCounter.CollectedResources >= _botHub.PriceList.CountResourceToBuildBotHub)
        {
            _assignedBotToBuild = _botHub.BotDispatcher.GetAvailableBot();

            _botHub.Flag.Deactivated += _assignedBotToBuild.ResetTasks;
            _assignedBotToBuild.AssignTasks(_buildingTask.CreateTask());;

            return;
        }

        if (_botHub.MineralRegistry.AvailableMineralsCount > 0)
        {
            CollectorBot collectorBot = _botHub.BotDispatcher.GetAvailableBot();
            collectorBot.AssignTasks(_miningTask.CreateTask());
        }
    }

    public override void Exit()
    {
        if (_assignedBotToBuild != null)
        {
            _botHub.Flag.Deactivated -= _assignedBotToBuild.ResetTasks;
            _assignedBotToBuild = null;
        }

        _botHub = null;
    }
}
