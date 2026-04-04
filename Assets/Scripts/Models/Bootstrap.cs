using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CollectorBot _prefab;
    [SerializeField] private CollectorBotBase _base;
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private CollectorBotFactory _fabricCollectorBot;
    [SerializeField] private CollectorBotBaseConfig _baseConfig;
    [SerializeField] private BuildProcessFactory _buildProcessFactory;

    [SerializeField] private CellRegister _cellRegister;
    [SerializeField] private ObjectPoolMineral _objectPullMineral;
    [SerializeField] private Map _map;

    [SerializeField] private MineralRegistry _mineralRegistry;

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BaseMenu _baseMenu;
    [SerializeField] private BaseMenuViewer _baseMenuViewer;
    [SerializeField] private BaseBuildButton _baseBuildButton;
    [SerializeField] private CollectorBotBaseFactory _collectorBotBaseFactory;
    [SerializeField] private ResourceCounterViewer _resourceCounterViewer;
    [SerializeField] private TimerViewer _timerViewer;
    [SerializeField] private MenuActivator _menuActivator;
    [SerializeField] private BuildProcessPool _buildProcessPool;
    [SerializeField] private FlagPlacer _flagPlacer;

    [SerializeField] private int _countStartBot = 3;

    private CollectorBaseService _collectorBaseService;
    private BaseMenuService _baseMenuService;
    private GridCreator _gridCreator;
    private Grid _grid;

    private bool _isInitialize = false;

    private void Awake()
    {
        _cellRegister.gameObject.SetActive(false);
        _mineralSpawner.gameObject.SetActive(false);
        _base.gameObject.SetActive(false);
        _inputReader.gameObject.SetActive(false);

        _inputReader.Initialize();

        _map.Initialize();
        _objectPullMineral.Initialize();

        _gridCreator = new GridCreator();
        _grid = _gridCreator.Create(_map);
        _cellRegister.Initialize(_grid);

        _mineralSpawner.Initialize(_coroutineRunner, _mineralRegistry);

        _fabricCollectorBot.Initialize(_prefab, _coroutineRunner);

        _menuActivator = new MenuActivator();
        _baseMenu = new BaseMenu(_timerViewer, _resourceCounterViewer, _baseMenuViewer, _baseBuildButton);
        _buildProcessPool.Initialize();

        _collectorBaseService = new CollectorBaseService(_coroutineRunner, _baseConfig, _mineralRegistry, _baseMenu, 
            _fabricCollectorBot, _collectorBotBaseFactory, _buildProcessPool);

        _collectorBotBaseFactory.Initialize(_collectorBaseService);

        _isInitialize = true;
    }

    private void OnEnable()
    {
        if (_isInitialize == false)
            return;

        _collectorBotBaseFactory.Created += OnBaseCreated;
        _baseMenu.OnActiveChanged += _menuActivator.SwitchActiveMenu;
        _inputReader.OnClick += OnSubscribeInputReader;
        _baseBuildButton.OnBuild += _buildProcessFactory.Create;
    }

    private void OnDisable()
    {
        if (_isInitialize == false)
            return;

        _collectorBotBaseFactory.Created -= OnBaseCreated;
        _baseMenu.OnActiveChanged -= _menuActivator.SwitchActiveMenu;
        _inputReader.OnClick -= OnSubscribeInputReader;
        _baseBuildButton.OnBuild -= _buildProcessFactory.Create;
    }

    private void Start()
    {
        CollectorBotBase collectorBase = _collectorBotBaseFactory.Create(new Vector3(0, 0, 0), true) as CollectorBotBase;
        
        for (int i = 0; i < _countStartBot; i++)
        {
            CollectorBot bot = _fabricCollectorBot.Create(collectorBase.SpawnBotPlace.position, true) as CollectorBot;
            collectorBase.BotDispatcher.EnqueueBot(bot);
        }
    }

    private void OnBaseCreated(ICollectorBase collectorBase)
    {
        collectorBase.Flag.Activated += _flagPlacer.SetFlag;
        collectorBase.Disabled += OnBaseDisabled;
        collectorBase.Click += _baseMenu.Show;
    }

    private void OnBaseDisabled(ICollectorBase collectorBase)
    {
        collectorBase.Flag.Activated -= _flagPlacer.SetFlag;
        collectorBase.Disabled -= OnBaseDisabled;
        collectorBase.Click -= _baseMenu.Show;
    }

    private void OnSubscribeInputReader(Transform transform)
    {
        _flagPlacer.TryInstalFlag(transform);
        _menuActivator.OnClosedMenu(transform);
    }
}