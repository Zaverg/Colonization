using System.Collections.Generic;
using UnityEngine;

public class MiningTask : Task
{
    private MineralRegistry _mineralRegistry;
    private ICoroutineRunner _coroutineRunner;

    private Vector3 _deliveryPosition;

    public MiningTask(MineralRegistry mineralRegistry, ICoroutineRunner coroutineRunner, Vector3 deliveryPosition)
    {
        _mineralRegistry = mineralRegistry;
        _coroutineRunner = coroutineRunner;

        _deliveryPosition = deliveryPosition;
    }

    public override Queue<CollectorBotTask> CreateTask()
    {
        IResource resource = _mineralRegistry.GetAvailableMineral();

        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, resource.Transform.position));
        tasks.Enqueue(new CollectorBotTask(StateType.Mining, resource: resource, coroutineRunner: _coroutineRunner));
        tasks.Enqueue(new CollectorBotTask(StateType.Taking, resource: resource));
        tasks.Enqueue(new CollectorBotTask(StateType.Moving, _deliveryPosition));
        tasks.Enqueue(new CollectorBotTask(StateType.Unloading));

        return tasks;
    }
}
