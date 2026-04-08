using System;
using UnityEngine;

[RequireComponent(typeof(CursorFollower))]
public class BuildProcess : MonoBehaviour, IClickable, IReleasable<BuildProcess>
{
    private float _buildTime;
    private IFactory _factory;
    private IStateMachine _builder;

    private Timer _timer;
    private CursorFollower _cursorFollower;

    private BoxCollider _previewColliderBuildObject;
    private Transform _previewBuildObject;

    // private Animator _animator;

    public event Action<ICreatable, IStateMachine> Completed;
    public event Action<BuildProcess> Released;

    public bool CanBuild { get; private set; } = true;

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Color red");
        CanBuild = false;
    }

    public void OnCollisionExit(Collision collision)
    {
        Debug.Log("Color green");
        CanBuild = true;
    }

    public void Initialize(Grid grid)
    {
        _cursorFollower = GetComponent<CursorFollower>();
        _cursorFollower.SetGrid(grid);
        _cursorFollower.enabled = true;
    }

    public void SetParams(IFactory factory, Action<ICreatable, IStateMachine> callBack, ICoroutineRunner coroutineRunner)
    {
        _factory = factory;
        Completed = callBack;
        _timer = new Timer(coroutineRunner);
    }

    public void SetParams(BuildProcessConfig config)
    {
        _previewBuildObject = Instantiate(config.Prefab);
        _previewColliderBuildObject = _previewBuildObject.GetComponent<BoxCollider>();
        _previewColliderBuildObject.enabled = false;
        _buildTime = config.BuildTime;

        _previewBuildObject.SetParent(transform);
        _previewBuildObject.transform.position = transform.position;
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

    public bool CanBuildd()
    {
       
        return true;
    }

    public void Release()
    {
        Destroy(_previewBuildObject.gameObject);
        Released?.Invoke(this);
    }

    private void FinishBuild()
    {
        ICreatable buildable = _factory.Create(transform.position, true);

        Completed?.Invoke(buildable, _builder);

        Release();
    }

    public void OnClick()
    {

    }
}