using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuildTask : CollectorBaseTask
{
    private ICollectorBase _collectorBase;
    private CollectorBotBaseFactory _baseFactory;
    private BuildProcessPool _buildProcessPool;

    public BaseBuildTask(ICollectorBase collectorBase, CollectorBotBaseFactory baseFactory, BuildProcessPool buildProcessPool)
    {
        _collectorBase = collectorBase;
        _baseFactory = baseFactory;
        _buildProcessPool = buildProcessPool;
    }

    public override Queue<CollectorBotTask> CreateTask()
    {
        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();

        Vector3 flagPosition = _collectorBase.Flag.transform.position;
        BuildProcess buildProcess = _buildProcessPool.GetBuildProcess();

        buildProcess.Completed += (IBuildable buildable, IStateMachine builder) =>
        {

        };
       
        tasks.Enqueue(new CollectorBotTask(StateType.Moving, flagPosition));
       // tasks.Enqueue(new CollectorBotTask(StateType.Building, buildObject: newCollectorBotBase));

        return tasks;
    }
}


public class BuildProcess : MonoBehaviour, IClickable, IReleasable<BuildProcess>
{
    private float _buildTime;
    private IFactory _factory;
    private Vector3 _buildPosition;
    private IStateMachine _builder;

    // private Animator _animator;

    public event Action<IBuildable, IStateMachine> Completed;
    public event Action<BuildProcess> Released;

    public void SetParams(IFactory factory, float buildTime, Vector3 buildPosition, Action<IBuildable, IStateMachine> callBack)
    {
        _buildTime = buildTime;
        _factory = factory;
        _buildPosition = buildPosition;
        Completed = callBack;
    }

    public void StartBuild(IStateMachine builder)
    {
        _builder = builder;
        Debug.Log($"Начало анимации c временем: {_buildTime}");
    }

    private void FinishBuild()
    {
        IBuildable buildable = _factory.Create(_buildPosition, true);

        Completed?.Invoke(buildable, _builder);

        // Возврат в пул
        Destroy(gameObject);
    }

    public void OnClick()
    {

    }
}

public interface IFactory
{
    public IBuildable Create(Vector3 position, bool visible);
}

public interface IBuildable
{

}

public class BuildProcessPool : ObjectPool<BuildProcess> 
{ 
    public BuildProcess GetBuildProcess()
    {
        return GetObject();
    }
}