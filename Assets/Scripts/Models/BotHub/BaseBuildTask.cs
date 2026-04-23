using System.Collections.Generic;
using UnityEngine;

public class BaseBuildTask : CollectorBaseTask
{
    private IBotHub _collectorBase;

    public BaseBuildTask(IBotHub collectorBase)
    {
        _collectorBase = collectorBase;
    }

    public override Queue<CollectorBotTask> CreateTask()
    {
        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();
        Flag flag = _collectorBase.Flag;
        BuildProcess buildProcess = flag.BuildProcess;

        Vector3 flagPosition = _collectorBase.Flag.transform.position;

        buildProcess.SetFinishCallback(OnBuildCompleted);

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, flagPosition));
        tasks.Enqueue(new CollectorBotTask(StateType.Building, buildProcess: buildProcess));

        return tasks;
    }

    private void OnBuildCompleted(Building building, IBot builder)
    {
        BotHub newBotHub = building as BotHub;
        CollectorBot bot = builder as CollectorBot;

        _collectorBase.Flag.Deactivate();
    
        _collectorBase.BotDispatcher.UnregisterBot(bot);
        newBotHub.BotDispatcher.EnqueueBot(bot);

        _collectorBase.ResourceCounter.Subtract(_collectorBase.CountResourceToBuildBase);
    }
}