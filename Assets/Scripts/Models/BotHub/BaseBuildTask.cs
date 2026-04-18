using System.Collections.Generic;
using UnityEngine;

public class BaseBuildTask : CollectorBaseTask
{
    private ICollectorBase _collectorBase;

    public BaseBuildTask(ICollectorBase collectorBase)
    {
        _collectorBase = collectorBase;
    }

    public override Queue<CollectorBotTask> CreateTask()
    {
        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();
        Flag flag = _collectorBase.Flag;
        BuildProcess buildProcess = flag.BuildProcess;

        Vector3 flagPosition = _collectorBase.Flag.transform.position;

        buildProcess.SetFinishCallBack(CallBack);

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, flagPosition));
        tasks.Enqueue(new CollectorBotTask(StateType.Building, buildProcess: buildProcess));

        return tasks;
    }

    private void CallBack(Building buildable, IStateMachine builder)
    {
        BotHub collectorBotBase = buildable as BotHub;
        CollectorBot collectorBot = builder as CollectorBot;

        _collectorBase.Flag.Deactivate();
    
        _collectorBase.BotDispatcher.FreeBot(collectorBot);
        collectorBotBase.BotDispatcher.EnqueueBot(collectorBot);

        _collectorBase.ResourceCounter.SubtractCounter(_collectorBase.CountResourceToBuildBase);
    }
}