using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CursorFollower))]
public class BuildProcess : MonoBehaviour, IClickable, IReleasable<BuildProcess>
{
    private BuildType _buildType;
    private float _buildTime;
    private IFactory _factory;
    private IStateMachine _builder;
    private List<BuildingShapeUnit> _shapes;

    private Timer _timer;
    private CursorFollower _cursorFollower;

    [SerializeField] private BoxCollider _previewCollider;
    [SerializeField] private Transform _preview;

    private Grid _grid;
    private Vector2Int _lastGridPosition;
    [SerializeField] private List<Vector2Int> _occupyArea;

    // private Animator _animator;

    public event Func<List<Vector2Int>> PositionChanged;
    public event Action<BuildProcess> Installed;
    public event Action<Building, IStateMachine> Completed;
    public event Action<BuildProcess> Released;

    public IReadOnlyList<BuildingShapeUnit> Shapes => _shapes;
    public Transform Transform => transform;
    public BuildType BuilderType => _buildType;

    public IReadOnlyList<Vector2Int> OccupyArea => _occupyArea;


    public void Update()
    {
        Vector2Int currentGridPosition = _grid.ConvertWorldToGridPosition(transform.position);

        if (_lastGridPosition != currentGridPosition)
        {
            _lastGridPosition = currentGridPosition;
            _occupyArea = PositionChanged?.Invoke();
        }
    }

    public void Initialize(Grid grid, ICoroutineRunner coroutineRunner)
    {
        _grid = grid;

        _cursorFollower = GetComponent<CursorFollower>();
        _cursorFollower.SetGrid(grid);
        _timer = new Timer(coroutineRunner);
    }

    public void SetConfig(BuildProcessConfig config, IFactory factory)
    {
        Debug.Log(config);
        _buildType = config.BuildType;
        _preview = Instantiate(config.Prefab);
        _buildTime = config.BuildTime;

        _previewCollider = _preview.GetComponent<BoxCollider>();
        _previewCollider.enabled = false;

        _preview.SetParent(transform);
        _preview.transform.position = transform.position;
        _shapes = GetComponentsInChildren<BuildingShapeUnit>().ToList();
    }

    public void SetFinishCallBack(Action<Building, IStateMachine> callBack)
    {
        Completed = callBack;
    }

    public Vector3 Install()
    {
        _cursorFollower.enabled = false;
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

    public void Interrupt()
    {
        _cursorFollower.enabled = true;

        if (_preview != null)
            _preview.gameObject.SetActive(true);
    }

    public void Release()
    {
        _cursorFollower.enabled = true;
        _preview.gameObject.SetActive(true);
        Released?.Invoke(this);
    }

    private void FinishBuild()
    {
        Building building = _factory.Create(transform.position, true);
        building.SetGridArea(_occupyArea);

        Completed?.Invoke(building, _builder);

        Release();
    }

    public void OnClick()
    {

    }
}
