using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CoroutineRunner _coroutineRunner;

    [Header("BotHub")]
    [SerializeField] private CollectorBotFactory _collectorBotFactory;
    [SerializeField] private BotHubFactory _botHubFactory;
    [SerializeField] private int _countStartBot = 3;

    [Header("Mineral")]
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private ObjectPoolMineral _mineralObjectPool;
    [SerializeField] private TimerViewer _timerViewerSpawn;

    [Header("Map")]
    [SerializeField] private CellRegister _cellRegister;
    [SerializeField] private Map _map;
    [SerializeField] private Grid _grid;

    [Header("BuildPlacer")]
    [SerializeField] private BuildProcessPlacer _buildProcessPlacer;
    [SerializeField] private BuildProcessFactory _buildProcessFactory;

    private GridCreator _gridCreator;

    private void Awake()
    {
        _inputReader.gameObject.SetActive(false);
        _mineralSpawner.gameObject.SetActive(false);
        _cellRegister.gameObject.SetActive(false);

        _map.Initialize();
        _gridCreator = new GridCreator();
        List<List<Cell>> grid = _gridCreator.Create(_map, _grid.CellSizeGrid);
        _grid.Initialize(grid);
        _cellRegister.Initialize(_grid);

        _mineralObjectPool.Initialize();
        _mineralSpawner.Initialize(_coroutineRunner);

        _buildProcessFactory.Initialize(_grid);
        _buildProcessPlacer.Initialize(_grid);

        _inputReader.Initialize();
    }

    private void OnEnable()
    {
        _mineralSpawner.Timer.Changed += _timerViewerSpawn.UpdateView;
    }

    private void OnDisable()
    {
        _mineralSpawner.Timer.Changed -= _timerViewerSpawn.UpdateView;
    }

    public void Start()
    {
        BuildProcess buildProcess = _buildProcessFactory.Create();
        buildProcess.transform.position = _map.transform.position;
        List<Vector2Int> gridPosition = _cellRegister.TryGetOccupyArea(buildProcess.CalculateArea());

        buildProcess.Install(gridPosition);

        BotHub botHub = _botHubFactory.Create(_map.transform.position, gridPosition);

        Destroy(buildProcess.gameObject);

        for (int i = 0; i < _countStartBot; i++)
        {
            CollectorBot bot = _collectorBotFactory.Create(botHub.SpawnBotPlace.position);
            botHub.BotDispatcher.EnqueueBot(bot);
        }
    }
}
