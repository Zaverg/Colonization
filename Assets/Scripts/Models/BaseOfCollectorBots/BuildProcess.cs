using System;
using UnityEngine;

[RequireComponent(typeof(CursorFollower))]
public class BuildProcess : MonoBehaviour, IClickable, IReleasable<BuildProcess>
{
    private float _buildTime;
    private IFactory _factory;
    private Vector3 _buildPosition;
    private IStateMachine _builder;

    private Timer _timer;
    private CursorFollower _cursorFollower;

    private BoxCollider _previewColliderBuildObject;
    private Transform _previewBuildObject;

    // private Animator _animator;

    public event Action<ICreatable, IStateMachine> Completed;
    public event Action<BuildProcess> Released;

    public void Initialize(Grid grid)
    {
        _cursorFollower = GetComponent<CursorFollower>();
        _cursorFollower.SetGrid(grid);
        _cursorFollower.enabled = true;
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
        _previewBuildObject = Instantiate(config.Prefab);
        _previewColliderBuildObject = _previewBuildObject.GetComponent<BoxCollider>();
        _previewColliderBuildObject.enabled = false;

        _previewBuildObject.SetParent(transform);
        _previewBuildObject.transform.position = Vector3.zero;
    }

    public Vector3 Install()
    {
        _cursorFollower.enabled = false;
        _previewColliderBuildObject.enabled = true;
        _previewBuildObject.gameObject.SetActive(false);

        return transform.position;
    }

    public void StartBuild(IStateMachine builder)
    {
        _previewBuildObject.gameObject.SetActive(true);
        _previewColliderBuildObject.enabled = true;
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

    public void Update()
    {
        if (_previewBuildObject == null)
        {
            Debug.LogError($"_previewBuildObject is NULL on {name}!", this);
            return;
        }
    }
}