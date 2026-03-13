using System;
using UnityEngine;

public class BuildProcess : MonoBehaviour, IClickable, IReleasable<BuildProcess>
{
    private float _buildTime;
    private IFactory _factory;
    private Vector3 _buildPosition;
    private IStateMachine _builder;

    private Timer _timer;
    // private Animator _animator;

    public event Action<ICreatable, IStateMachine> Completed;
    public event Action<BuildProcess> Released;

    public void SetParams(IFactory factory, float buildTime, Vector3 buildPosition, Action<ICreatable, IStateMachine> callBack,
        ICoroutineRunner coroutineRunner)
    {
        _buildTime = buildTime;
        _factory = factory;
        _buildPosition = buildPosition;
        Completed = callBack;
        _timer = new Timer(coroutineRunner);
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