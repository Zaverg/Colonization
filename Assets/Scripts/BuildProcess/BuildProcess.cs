using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CursorFollower))]
public class BuildProcess : MonoBehaviour
{
    private float _buildDutation;

    private BoxCollider _collider;
    private BotHubFactory _factory;
    private IBot _builder;

    private Timer _timer;
    private CursorFollower _cursorFollower;
    private Canvas _canvas;

    private Material _previewMaterial;
    private Transform _preview;
    private BotHubBuildingAnimation _buildingAnimation;

    private List<Vector2Int> _occupiedArea = new List<Vector2Int>();

    public event Action<BotHub, IBot> Completed;
    public event Action<BuildProcess> Released;

    public IReadOnlyList<Vector2Int> OccupyArea => _occupiedArea;
    public Timer Timer => _timer;
    public bool IsBuilding { get; private set; } = false;

    public void OnDisable()
    {
        Released?.Invoke(this);
    }

    public void Initialize(IGrid grid, ICoroutineRunner coroutineRunner, BuildProcessConfig config, BotHubFactory factory)
    {
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        _cursorFollower = GetComponent<CursorFollower>();
        _cursorFollower.SetGrid(grid);
        _timer = new Timer(coroutineRunner);

        _preview = Instantiate(config.Prefab);
        _buildDutation = config.BuildTime;
        _buildingAnimation = config.BuildingAnimation;

        _previewMaterial = _preview.GetComponent<MeshRenderer>().material;
        _collider.size = new Vector3(_preview.transform.localScale.x, _preview.transform.localScale.y, _preview.localScale.z);

        _preview.SetParent(transform);
        _preview.transform.position = transform.position;

        _canvas = GetComponentInChildren<Canvas>(true);
        
        _factory = factory;
    }

    public List<Vector3> CalculateArea()
    {
        List<Vector3> area = new List<Vector3>();

        area.Add(new Vector3(transform.position.x + _preview.transform.localScale.x / 2, transform.position.y, transform.position.z - _preview.transform.localScale.z / 2));
        area.Add(new Vector3(transform.position.x - _preview.transform.localScale.x / 2, transform.position.y, transform.position.z + _preview.transform.localScale.z / 2));

        return area;
    }

    public void SetOnCompleteCallback(Action<BotHub, IBot> callback)
    {
        Completed = callback;
    }

    public void Install(List<Vector2Int> occupuArea)
    {
        _cursorFollower.enabled = false;
        _preview.gameObject.SetActive(false);

        _occupiedArea = occupuArea;
    }

    public void StartBuild(IBot builder)
    {
        IsBuilding = true;
        _builder = builder;

        float buildDuration = _buildDutation * _builder.Builder.BuildSpeedСoefficient;

        if (_canvas != null)
            _canvas.gameObject.SetActive(true);

        _preview.gameObject.SetActive(true);
        _collider.enabled = true;

        _timer.Ended += FinishBuild;
        _timer.SetDuration(buildDuration);
        _timer.Run();

        _buildingAnimation.StartAnimation(_previewMaterial, buildDuration);
    }

    private void FinishBuild()
    {
        _timer.Ended -= FinishBuild;

        BotHub botHub = _factory.Create(transform.position, _occupiedArea);

        Completed?.Invoke(botHub, _builder);

        gameObject.SetActive(false);
    }
}