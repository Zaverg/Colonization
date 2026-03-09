using System.Collections.Generic;
using UnityEngine;

public class BaseBuildTask : CollectorBaseTask
{
    private ICollectorBase _collectorBase;
    private CollectorBotBaseFactory _baseFactory;

    public BaseBuildTask(ICollectorBase collectorBase, CollectorBotBaseFactory baseFactory)
    {
        _collectorBase = collectorBase;
        _baseFactory = baseFactory;
    }

    public override Queue<CollectorBotTask> CreateTask()
    {
        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();

        Vector3 flagPosition = _collectorBase.Flag.transform.position;
        CollectorBotBase newCollectorBotBase = _baseFactory.Create(flagPosition, false);

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, flagPosition));
        tasks.Enqueue(new CollectorBotTask(StateType.Building, collectorBase: newCollectorBotBase));

        return tasks;
    }
}