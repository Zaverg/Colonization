using System.Collections.Generic;
using UnityEngine;

public class BaseBuildTask : CollectorBaseTask
{
    private ICollectorBase _collectorBase;
    private CollectorBotBaseFactory _baseFactory;
    private BuildProcessPool _buildProcessPool;
    private ICoroutineRunner _coroutineRunner;

    public BaseBuildTask(ICollectorBase collectorBase, CollectorBotBaseFactory baseFactory, BuildProcessPool buildProcessPool, 
        ICoroutineRunner coroutineRunner)
    {
        _collectorBase = collectorBase;
        _baseFactory = baseFactory;
        _buildProcessPool = buildProcessPool;
        _coroutineRunner = coroutineRunner;
    }

    public override Queue<CollectorBotTask> CreateTask()
    {
        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();

        Vector3 flagPosition = _collectorBase.Flag.transform.position;

        BuildProcess buildProcess = _buildProcessPool.GetBuildProcess();
        buildProcess.transform.position = _collectorBase.Flag.transform.position;
        buildProcess.gameObject.SetActive(false);

        buildProcess.SetParams(_baseFactory, 5f, flagPosition, CallBack, _coroutineRunner);

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, flagPosition));
        tasks.Enqueue(new CollectorBotTask(StateType.Building, buildProcess: buildProcess));

        return tasks;
    }

    private void CallBack(ICreatable buildable, IStateMachine builder)
    {
        CollectorBotBase collectorBotBase = buildable as CollectorBotBase;
        CollectorBot collectorBot = builder as CollectorBot;

        _collectorBase.Flag.Deactivate();
    
        _collectorBase.BotDispatcher.FreeBot(collectorBot);
        collectorBotBase.BotDispatcher.EnqueueBot(collectorBot);

        _collectorBase.ResourceCounter.SubtractCounter(_collectorBase.CountResourceToBuildBase);
    }
}