using System.Collections.Generic;
using UnityEngine;

public class MiningTask : CollectorBaseTask
{
    private MineralRegistry _mineralRegistry;
    private CollectorBotDispatcher _collectorBotDispatcher;
    private ICoroutineRunner _coroutineRunner;

    private Vector3 _deliveryPos;

    public MiningTask(MineralRegistry mineralRegistry, CollectorBotDispatcher collectorBotDispatcher, ICoroutineRunner coroutineRunner, Vector3 deliveryPos)
    {
        _mineralRegistry = mineralRegistry;
        _collectorBotDispatcher = collectorBotDispatcher;
        _coroutineRunner = coroutineRunner;

        _deliveryPos = deliveryPos;
    }
    
    public override Queue<CollectorBotTask> CreateTask()
    {
        IResource mineral = _mineralRegistry.GetAvailableMineral();

        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, mineral.Transform.position));
        tasks.Enqueue(new CollectorBotTask(StateType.Mining, mineral: mineral, coroutineStarter: _coroutineRunner));
        tasks.Enqueue(new CollectorBotTask(StateType.Taking, mineral: mineral));
        tasks.Enqueue(new CollectorBotTask(StateType.Moving, _deliveryPos));
        tasks.Enqueue(new CollectorBotTask(StateType.Dropping));

        return tasks;
    }
}
