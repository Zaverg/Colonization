using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CursorFollower))]
public class BuildProcess : MonoBehaviour, IClickable, IReleasable<BuildProcess>
{
    private float _buildTime;
    private IFactory _factory;
    private IStateMachine _builder;
    private List<BuildingShapeUnit> _shapes;

    private Timer _timer;
    private CursorFollower _cursorFollower;

    private BoxCollider _previewCollider;
    private Transform _preview;

    private Grid _grid;
    private Vector2Int _lastGridPosition;

    // private Animator _animator;

    public event Action PositionChanged;
    public event Action<BuildProcess> Installed;
    public event Action<ICreatable, IStateMachine> Completed;
    public event Action<BuildProcess> Released;

    public IReadOnlyList<BuildingShapeUnit> Shapes => _shapes;

    public void Update()
    {
        Vector2Int currentGridPosition = _grid.ConvertWorldToGridPosition(transform.position);

        if (_lastGridPosition != currentGridPosition)
        {
            _lastGridPosition = currentGridPosition;
            PositionChanged?.Invoke();
        }
    }

    public void Initialize(BuildProcessConfig config, Grid grid, ICoroutineRunner coroutineRunner)
    {
        _grid = grid;
        _cursorFollower = GetComponent<CursorFollower>();
        _cursorFollower.SetGrid(grid);

        _timer = new Timer(coroutineRunner);

        _preview = Instantiate(config.Prefab);
        _previewCollider = _preview.GetComponent<BoxCollider>();
        _previewCollider.enabled = false;
        _buildTime = config.BuildTime;

        _preview.SetParent(transform);
        _preview.transform.position = transform.position;

        _shapes = GetComponentsInChildren<BuildingShapeUnit>().ToList();
    }

    public void SetParams(Action<ICreatable, IStateMachine> callBack, IFactory factory)
    {
        Completed = callBack;
        _factory = factory;
    }

    public Vector3 Install()
    {
        _cursorFollower.enabled = false;
        _previewCollider.enabled = true;
        _preview.gameObject.SetActive(false);

        return transform.position;
    }

    public void StartBuild(IStateMachine builder)
    {
        _preview.gameObject.SetActive(true);
        _previewCollider.enabled = true;
        _builder = builder;

        _timer.Ended += FinishBuild;
        _timer.SetDuration(_buildTime);

        _timer.Run();
        Debug.Log($"Начало анимации c временем: {_buildTime}");
    }

    public void Release()
    {
        _cursorFollower.enabled = true;
        Destroy(_preview.gameObject);
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