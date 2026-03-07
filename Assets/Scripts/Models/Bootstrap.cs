using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CollectorBot _prefab;
    [SerializeField] private CollectorBotBase _base;
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private CollectorBotFactory _fabricCollectorBot;
    [SerializeField] private CollectorBotBaseConfig _baseConfig;

    [SerializeField] private SpawnGrid _cellRegister;
    [SerializeField] private ObjectPoolMineral _objectPullMineral;
    [SerializeField] private Map _map;

    [SerializeField] private MineralRegistry _mineralRegistry;

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BaseMenu _baseMenu;
    [SerializeField] private BaseMenuViewer _baseMenuViewer;
    [SerializeField] private FlagButton _flagButton;
    [SerializeField] private CollectorBotBaseFactory _collectorBotBaseFactory;
    [SerializeField] private ResourceCounterViewer _resourceCounterViewer;
    [SerializeField] private TimerViewer _timerViewer;
    [SerializeField] private MenuActivator _menuActivator;
    [SerializeField] private FlagSpawner _flagSpawner;

    private int _countStartBot = 3;

    private CollectorBaseService _collectorBaseService;
    private BaseMenuService _baseMenuService;

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
        _cellRegister.Initialize();

        _mineralSpawner.Initialize(_coroutineRunner, _mineralRegistry);

        _fabricCollectorBot.Initialize(_prefab, _coroutineRunner);

        _menuActivator = new MenuActivator();
        _baseMenu = new BaseMenu(_timerViewer, _resourceCounterViewer, _baseMenuViewer, _flagButton);
        _baseMenu.OnActiveChanged += _menuActivator.SwitchActiveMenu;

        _collectorBaseService = new CollectorBaseService(_coroutineRunner, _baseConfig, _mineralRegistry, _baseMenu, _fabricCollectorBot);

        _collectorBotBaseFactory.Initialize(_collectorBaseService);

        _isInitialize = true;
    }

    private void OnEnable()
    {
        if (_isInitialize == false)
            return;

        _baseMenu.OnActiveChanged += _menuActivator.SwitchActiveMenu;
        _collectorBotBaseFactory.Created += _flagSpawner.Spawn;
    }

    private void OnDisable()
    {
        if (_isInitialize == false)
            return;

        _baseMenu.OnActiveChanged -= _menuActivator.SwitchActiveMenu;
        _collectorBotBaseFactory.Created -= _flagSpawner.Spawn;
    }

    private void Start()
    {
        CollectorBotBase collectorBase = _collectorBotBaseFactory.Create(new Vector3(0, 0, 0));

        for (int i = 0; i < _countStartBot; i++)
        {
            CollectorBot bot = _fabricCollectorBot.Create();
            collectorBase.BotDispatcher.EnqueueCollector(bot);
        }
    }
}