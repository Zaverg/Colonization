using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CollectorBot _prefab;
    [SerializeField] private BotHub _base;
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private CollectorBotSpawner _collectorBotSpawner;
    [SerializeField] private CollectorBotBaseConfig _baseConfig;
    [SerializeField] private BuildProcessSpawner _buildProcessSpawner;

    [SerializeField] private CellRegister _cellRegister;
    [SerializeField] private ObjectPoolMineral _mineralObjectPool;
    [SerializeField] private Map _map;
    [SerializeField] private Grid _grid;

    [SerializeField] private MineralRegistry _mineralRegistry;

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BaseMenu _baseMenu;
    [SerializeField] private BaseMenuViewer _baseMenuViewer;
    [SerializeField] private BotHubFactory _botHubFactory;
    [SerializeField] private ResourceCounterViewer _resourceCounterViewer;
    [SerializeField] private TimerViewer _timerViewer;
    [SerializeField] private MenuActivator _menuActivator;
    [SerializeField] private BuildProcessPool _buildProcessPool;
    [SerializeField] private BuildProcessPlacer _buildProcessPlacer;

    [SerializeField] private int _countStartBot = 3;

    private BaseService _collectorBaseService;
    private GridCreator _gridCreator;

    private bool _isInitialized = false;

    private void Awake()
    {
        _cellRegister.gameObject.SetActive(false);
        _mineralSpawner.gameObject.SetActive(false);
        _base.gameObject.SetActive(false);
        _inputReader.gameObject.SetActive(false);

        _inputReader.Initialize();

        _map.Initialize();
        _mineralObjectPool.Initialize();

        _gridCreator = new GridCreator();
        List<List<Cell>> grid = _gridCreator.Create(_map, _grid.CellSizeGrid);

        _grid.Initialize(grid);
        _cellRegister.Initialize();

        _mineralSpawner.Initialize(_coroutineRunner, _mineralRegistry);

        _collectorBotSpawner.Initialize(_prefab, _coroutineRunner);

        _menuActivator = new MenuActivator();
        _buildProcessPool.Initialize();

        _collectorBaseService = new BaseService(_coroutineRunner, _baseConfig, _mineralRegistry, _baseMenu, 
            _collectorBotSpawner, _botHubFactory, _buildProcessPool);

        _botHubFactory.Initialize(_collectorBaseService);

        _isInitialized = true;
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
            return;

        _botHubFactory.Created += OnBaseCreated;
        _baseMenu.OnActiveChanged += _menuActivator.SwitchActiveMenu;
        _inputReader.OnClick += HandleInputClick;
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            return;

        _botHubFactory.Created -= OnBaseCreated;
        _baseMenu.OnActiveChanged -= _menuActivator.SwitchActiveMenu;
        _inputReader.OnClick -= HandleInputClick;
    }

    private void Start()
    {
        BotHub botHub = _botHubFactory.Create(new Vector3(0, 0, 0), true) as BotHub;
        
        for (int i = 0; i < _countStartBot; i++)
        {
            CollectorBot bot = _collectorBotSpawner.Spawn(botHub.SpawnBotPlace.position, true);
            botHub.BotDispatcher.EnqueueBot(bot);
        }
    }

    private void OnBaseCreated(IBotHub collectorBase)
    {
        collectorBase.Disabled += OnBaseDisabled;
        collectorBase.Click += _baseMenu.Show;
    }

    private void OnBaseDisabled(IBotHub collectorBase)
    {
        collectorBase.Disabled -= OnBaseDisabled;
        collectorBase.Click -= _baseMenu.Show;
    }

    private void HandleInputClick(Transform transform)
    {
        _menuActivator.OnClosedMenu(transform);
    }
}