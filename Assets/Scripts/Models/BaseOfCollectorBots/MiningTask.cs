using System.Collections.Generic;

public class MiningTask : CollectorBaseTask
{
    private MineralRegistry _mineralRegistry;
    private CollectorBotDispatcher _collectorBotDispatcher;
    private CoroutineRunner _coroutineRunner;

    public MiningTask()
    {

    }
    
    public override void AssignTask(CollectorBot collector)
    {
        if (_mineralRegistry.AvailableMineralsCount == 0)
        {
            _collectorBotDispatcher.EnqueueCollector(collector);

            return;
        }

        IResource mineral = _mineralRegistry.GetAvailableMineral();

        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, mineral.Transform.position));
        tasks.Enqueue(new CollectorBotTask(StateType.Mining, mineral: mineral, coroutineStarter: _coroutineRunner));
        tasks.Enqueue(new CollectorBotTask(StateType.Taking, mineral: mineral));
        tasks.Enqueue(new CollectorBotTask(StateType.Moving, collector.transform.position));
        tasks.Enqueue(new CollectorBotTask(StateType.Dropping));

        collector.AssignTasks(tasks);
    }
}
