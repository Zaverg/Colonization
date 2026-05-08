using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CursorFollower))]
public class BuildProcess : MonoBehaviour, IReleasable<BuildProcess>
{
    private BuildType _buildType;
    private float _buildDutation;

    private BoxCollider _collider;
    private BuildFactory _factory;
    private IBot _builder;

    private Timer _timer;
    private CursorFollower _cursorFollower;

    private Material _previewMaterial;
    private Transform _preview;
    private BotHubBuildingAnimation _buildingAnimation;

    private List<Vector2Int> _occupiedArea = new List<Vector2Int>();

    public event Action<Building, IBot> Completed;
    public event Action<BuildProcess> Released;

    public BuildType BuilderType => _buildType;
    public IReadOnlyList<Vector2Int> OccupyArea => _occupiedArea;
    public Timer Timer => _timer;

    public void Initialize(Grid grid, ICoroutineRunner coroutineRunner)
    {
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        _cursorFollower = GetComponent<CursorFollower>();
        _cursorFollower.SetGrid(grid);
        _timer = new Timer(coroutineRunner);
    }

    public void SetConfig(BuildProcessConfig config, BuildFactory factory)
    {
        _buildType = config.BuildType;
        _preview = Instantiate(config.Prefab);
        _buildDutation = config.BuildTime;
        _buildingAnimation = config.BuildingAnimation;

        _previewMaterial = _preview.GetComponent<MeshRenderer>().material;
        _collider.size = new Vector3(_preview.transform.localScale.x, _preview.transform.localScale.y, _preview.localScale.z);

        _preview.SetParent(transform);
        _preview.transform.position = transform.position;

        _factory = factory;
    }

    public List<Vector3> CalculateArea()
    {
        List<Vector3> area = new List<Vector3>();

        area.Add(new Vector3(transform.position.x + _preview.transform.localScale.x / 2, transform.position.y, transform.position.z - _preview.transform.localScale.z / 2));
        area.Add(new Vector3(transform.position.x - _preview.transform.localScale.x / 2, transform.position.y, transform.position.z + _preview.transform.localScale.z / 2));

        return area;
    }

    public void SetOnCompleteCallback(Action<Building, IBot> callback)
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
        gameObject.SetActive(true);

        _builder = builder;

        float buildDuration = _buildDutation * _builder.Builder.BuildSpeedСoefficient;

        _preview.gameObject.SetActive(true);
        _collider.enabled = true;

        _timer.Ended += FinishBuild;
        _timer.SetDuration(buildDuration);
        _timer.Run();

        _buildingAnimation.StartAnimation(_previewMaterial, buildDuration);
    }

    public void Release()
    {
        _cursorFollower.enabled = true;
        _collider.enabled = false;
        _occupiedArea.Clear();

        Destroy(_preview.gameObject);
        Released?.Invoke(this);
    }

    private void FinishBuild()
    {
        _timer.Ended -= FinishBuild;

        Building building = _factory.Create(transform.position, _occupiedArea);

        Completed?.Invoke(building, _builder);

        Release();
    }
}