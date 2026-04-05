using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CursorFollower))]
public class BuildProcess : MonoBehaviour, IClickable, IReleasable<BuildProcess>
{
    private float _buildTime;
    private IFactory _factory;
    private Vector3 _buildPosition;
    private IStateMachine _builder;

    private List<BuildingShapeUnit> _shapes = new List<BuildingShapeUnit>();
    private Timer _timer;
    // private Animator _animator;

    public event Action<ICreatable, IStateMachine> Completed;
    public event Action<BuildProcess> Released;

    public void Inicialize(Grid grid)
    {
        if (_shapes.Count != 0) 
            return;
        
        _shapes = GetComponentsInChildren<BuildingShapeUnit>().ToList();

        GetComponent<CursorFollower>().SetGrid(grid);
        
    }

    public void SetParams(IFactory factory, float buildTime, Vector3 buildPosition, Action<ICreatable, IStateMachine> callBack,
        ICoroutineRunner coroutineRunner)
    {
        _buildTime = buildTime;
        _factory = factory;
        _buildPosition = buildPosition;
        Completed = callBack;
        _timer = new Timer(coroutineRunner);
    }

    public void SetParams(BuildProcessConfig config)
    {
        int count = config.ShapeLocalPosition.Count;

        for (int i = 0; i < count; i++)
        {
            if (i < _shapes.Count)
                _shapes[i].transform.localPosition = config.ShapeLocalPosition[i];
        }

        transform.localScale = config.Scale;
        transform.rotation = Quaternion.Euler(config.Rotation);
    }

    public void StartBuild(IStateMachine builder)
    {
        _builder = builder;

        _timer.Ended += FinishBuild;
        _timer.SetDuration(_buildTime);

        _timer.Run();
        Debug.Log($"Начало анимации c временем: {_buildTime}");
    }

    private void FinishBuild()
    {
        ICreatable buildable = _factory.Create(_buildPosition, true);

        Completed?.Invoke(buildable, _builder);

        Released?.Invoke(this);
    }

    public void OnClick()
    {

    }
}