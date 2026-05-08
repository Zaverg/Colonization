using System.Collections.Generic;
using UnityEngine;

public class BuildTask : Task
{
    private IBotHub _botHub;

    public BuildTask(IBotHub botHub)
    {
        _botHub = botHub;
    }

    public override Queue<CollectorBotTask> CreateTask()
    {
        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();
        Flag flag = _botHub.Flag;
        BuildProcess buildProcess = flag.BuildProcess;

        Vector3 flagPosition = _botHub.Flag.transform.position;

        buildProcess.SetOnCompleteCallback(OnBuildCompleted);

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, flagPosition));
        tasks.Enqueue(new CollectorBotTask(StateType.Building, buildProcess: buildProcess));
        tasks.Enqueue(new CollectorBotTask(StateType.Idle));

        return tasks;
    }
    
    private void OnBuildCompleted(Building building, IBot builder)
    {
        BotHub newBotHub = building as BotHub;
        CollectorBot bot = builder as CollectorBot;

        _botHub.Flag.Deactivated -= bot.ResetTasks;
        _botHub.Flag.Deactivate();
    
        _botHub.BotDispatcher.UnregisterBot(bot);
        newBotHub.BotDispatcher.EnqueueBot(bot);

        _botHub.ResourceCounter.Subtract(_botHub.PriceList.CountResourceToBuildBotHub);
    }
}