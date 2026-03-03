using UnityEngine;
using System.Collections.Generic;

public class CollectorBotBase : MonoBehaviour, IClickable, ICollectorBase
{
    [SerializeField] private List<CollectorBot> _collectors = new List<CollectorBot>();
    [SerializeField] private int _countResourceToCreateBot;
    [SerializeField] private int _countResourceToBuildBase;

    private CollectorBaseState _currentState;
    private ExtractionState _extractionState;
    private FlagPlaceState _flagPlaceState;

    private CollectorBotDispatcher _collectorBotDispatcher;
    private CollectorBotBaseConfig _config;
    private MineralRegistry _mineralRegistry;
    private Scanner _scanner;
    private BaseMenu _baseMenu;
    private CollectorBotFactory _fabricCollectorBot;

    private Timer _timer;
    private ResourceCounter _resurceCounter;

    public Timer Timer => _timer;
    public ResourceCounter ResurceCounter => _resurceCounter;
    
    public BaseMenu Stats => _baseMenu;
    public int CountResurceToCreateBot => _countResourceToCreateBot;
    public int CountResurceToBuildBase => _countResourceToBuildBase;
    public CollectorBotDispatcher BotDispatcher => _collectorBotDispatcher;

    private void OnEnable()
    {
        if (_baseMenu == null)
            return;

        _timer.Ended += ActivateScanner;
        _scanner.Detected += _mineralRegistry.Register;
        _resurceCounter.MineralCountChanged += OnValidateCountResource;

        foreach (CollectorBot collector in _collectors)
        {
            collector.OnBotAvailable += _collectorBotDispatcher.EnqueueCollector;
            //collector.Unloader.Unloaded += _resurceCounter.UpdateCounter;
        }
    }

    private void OnDisable()
    {
        if (_baseMenu == null)
            return;

        _timer.Ended -= ActivateScanner;
        _scanner.Detected -= _mineralRegistry.Register;
        _resurceCounter.MineralCountChanged -= OnValidateCountResource;

        foreach (CollectorBot collector in _collectors)
        {
            collector.OnBotAvailable -= _collectorBotDispatcher.EnqueueCollector;
            //collector.Unloader.Unloaded -= _resurceCounter.UpdateCounter;
        }
    }

    private void Start()
    {
        _timer.Run();
    }

    private void Update()
    {
        if (_currentState == null)
            return;

       _currentState.Run();
    }

    public void Initialize(CollectorBaseService collectorBaseService, BaseMenuService baseMenuService)
    {
        _baseMenu = new BaseMenu(baseMenuService.TimerViewer, baseMenuService.ResourceCounterViewer, baseMenuService.MenuActivator);
        _config = collectorBaseService.Config;

        _scanner = new Scanner(transform.position, _config.ScanLayer, _config.ScanRadius);

        _timer = new Timer(collectorBaseService.CoroutineRunner);

        _collectorBotDispatcher = new CollectorBotDispatcher();

        gameObject.SetActive(true);
    }

    public void OnClick()
    {
        _baseMenu.Show(this);
    }

    public void PlaceFlag(Flag flag)
    {
        SwitchState(_flagPlaceState);
    }

    public void SwitchState(CollectorBaseState state)
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Entry(this);
    }

    public void OnValidateCountResource(int count)
    {
        if (count != 0 && count % _countResourceToCreateBot == 0)
            CreateCollectorBot();
    }

    private void CreateCollectorBot()
    {
        CollectorBot collectorBot = _fabricCollectorBot.Create();
        //collectorBot.Unloader.Unloaded += _resurceCounter.UpdateCounter;

        _collectorBotDispatcher.EnqueueCollector(collectorBot);
        _collectors.Add(collectorBot);
    }

    private void ActivateScanner()
    {
        _scanner.Scan();
        _timer.Run();
    }
}