using UnityEngine;
using System;

public class CollectorBotBase : MonoBehaviour, IClickable, ICollectorBase
{
    [SerializeField] private int _countResourceToCreateBot = 3;
    [SerializeField] private int _countResourceToBuildBase = 5;
    private float _scanTime = 5;

    private CollectorBaseState _currentState;
    private ExtractionState _extractionState;
    private FlagPlaceState _flagPlaceState;

    private CollectorBotDispatcher _collectorBotDispatcher;
    private CollectorBotBaseConfig _config;
    private MineralRegistry _mineralRegistry;
    private Scanner _scanner;
    private CollectorBotFactory _fabricCollectorBot;
    private Timer _timer;
    private ResourceCounter _resourceCounter;
    private Flag _flag;

    public event Action<CollectorBotBase> Click;

    public Timer Timer => _timer;
    public ResourceCounter ResourceCounter => _resourceCounter;
    public int CountResourceToCreateBot => _countResourceToCreateBot;
    public int CountResourceToBuildBase => _countResourceToBuildBase;
    public CollectorBotDispatcher BotDispatcher => _collectorBotDispatcher;
    public Flag Flag => _flag;
    public MineralRegistry MineralRegistry => _mineralRegistry;

    private void OnEnable()
    {
        _timer.Ended += ActivateScanner;
        _scanner.Detected += _mineralRegistry.Register;
        _resourceCounter.MineralCountChanged += TryCreateCollectorBot;

        if (_flag != null)
            _flag.Installed += PlaceFlag;
    }

    private void OnDisable()
    {
        _timer.Ended -= ActivateScanner;
        _scanner.Detected -= _mineralRegistry.Register;
        _resourceCounter.MineralCountChanged -= TryCreateCollectorBot;

        if (_flag != null)
            _flag.Installed -= PlaceFlag;
    }

    private void Start()
    {
        _currentState.Entry(this);
        _timer.Run();
    }

    private void Update()
    {
        Debug.Log(_collectorBotDispatcher.AvailableCollectorsCount);

        if (_currentState == null)
            return;

        Debug.Log(_collectorBotDispatcher);
       _currentState.Run();
    }

    public void Initialize(CollectorBaseService collectorBaseService)
    {
        _config = collectorBaseService.Config;
        _mineralRegistry = collectorBaseService.MineralRegistry;
        _resourceCounter = new ResourceCounter();

        _scanner = new Scanner(transform.position, _config.ScanLayer, _config.ScanRadius);

        _timer = new Timer(collectorBaseService.CoroutineRunner);
        _timer.SetDuration(_scanTime);

        _fabricCollectorBot = collectorBaseService.CollectorBotFactory;
        _collectorBotDispatcher = new CollectorBotDispatcher(_resourceCounter);

        MiningTask miningTask = new MiningTask(_mineralRegistry, _collectorBotDispatcher, collectorBaseService.CoroutineRunner, transform.position);
        BaseBuildTask baseBuildTask = new BaseBuildTask();

        _extractionState = new ExtractionState(miningTask, _fabricCollectorBot);
        _flagPlaceState = new FlagPlaceState(miningTask, baseBuildTask);
        
        _currentState = _extractionState;
    }

    public void OnClick()
    {
        Debug.Log("OnClick");

        Click?.Invoke(this);
    }

    public void PlaceFlag()
    {
        SwitchState(_flagPlaceState);
    }

    public void SetFlag(Flag flag)
    {
        if (flag == null)
            return;

        _flag = flag;

        _flag.Installed -= PlaceFlag;
        _flag.Installed += PlaceFlag;
    }

    public void SwitchState(CollectorBaseState state)
    {
        Debug.Log("SwitchState");

        _currentState.Exit();
        _currentState = state;
        _currentState.Entry(this);
    }

    public void TryCreateCollectorBot(int countResource)
    {
        if (countResource == 0)
            return;

        if (countResource < _countResourceToCreateBot)
            return;

        CollectorBot collectorBot = _fabricCollectorBot.Create();
        _collectorBotDispatcher.EnqueueCollector(collectorBot);

        _resourceCounter.SubtractCounter(countResource);

        Debug.Log("Create bot");
    }

    private void ActivateScanner()
    {
        _scanner.Scan();
        _timer.Run();
    }
}