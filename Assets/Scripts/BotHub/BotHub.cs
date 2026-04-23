using UnityEngine;
using System;
using System.Collections.Generic;

public class BotHub : Building, IClickable, IBotHub
{
    [SerializeField] private int _countResourceToCreateBot = 3;
    [SerializeField] private int _countResourceToBuildBase = 5;
    [SerializeField] private Flag _flag;
    [SerializeField] private Transform _spawnBotPlace;

    [SerializeField] private float _scanInterval = 5;

    private BaseState _currentState;
    private ExtractionState _extractionState;
    private FlagPlaceState _flagPlaceState;

    private Dictionary<CollectorBotTaskName, CollectorBaseTask> _tasks = new Dictionary<CollectorBotTaskName, CollectorBaseTask>();
    private CollectorBaseTask _mainTask;

    private BotDispatcher _botDispatcher;
    private CollectorBotBaseConfig _config;
    private MineralRegistry _mineralRegistry;
    private Scanner _scanner;
    private CollectorBotSpawner _collectorBotSpawner;
    private Timer _timer;
    private ResourceCounter _resourceCounter;

    public event Action<IBotHub> Click;
    public event Action<IBotHub> Disabled;

    public Timer Timer => _timer;
    public ResourceCounter ResourceCounter => _resourceCounter;
    public int CountResourceToCreateBot => _countResourceToCreateBot;
    public int CountResourceToBuildBase => _countResourceToBuildBase;
    public BotDispatcher BotDispatcher => _botDispatcher;
    public Flag Flag => _flag;
    public MineralRegistry MineralRegistry => _mineralRegistry;
    public CollectorBotSpawner CollectorBotSpawner => _collectorBotSpawner;
    public Transform SpawnBotPlace => _spawnBotPlace;
    public CollectorBaseTask MainTask => _mainTask;

    private void OnEnable()
    {
        _timer.Ended += ActivateScanner;
        _scanner.Detected += _mineralRegistry.Register;

        if (_flag == null)
            return;

        _flag.Installed += OnFlagInstalled;
        _flag.Deactivated += OnFlagDeactivated;
    }

    private void OnDisable()
    {
        _timer.Ended -= ActivateScanner;
        _scanner.Detected -= _mineralRegistry.Register;

        if (_flag == null)
            return;

        _flag.Installed -= OnFlagInstalled;
        _flag.Deactivated -= OnFlagDeactivated;

        Disabled?.Invoke(this);
    }

    private void Start()
    {
        _currentState.Entry(this);
        _timer.Run();
    }

    private void Update()
    {
        if (_currentState == null)
            return;

       _currentState.Run();
    }

    public void Initialize(BaseService collectorBaseService)
    {
        _config = collectorBaseService.Config;
        _mineralRegistry = collectorBaseService.MineralRegistry;
        _resourceCounter = new ResourceCounter();

        _scanner = new Scanner(transform.position, _config.ScanLayer, _config.ScanRadius);

        _timer = new Timer(collectorBaseService.CoroutineRunner);
        _timer.SetDuration(_scanInterval);

        _collectorBotSpawner = collectorBaseService.CollectorBotSpawner;
        _botDispatcher = new BotDispatcher(_resourceCounter);

        MiningTask miningTask = new MiningTask(_mineralRegistry, collectorBaseService.CoroutineRunner, transform.position);
        BaseBuildTask baseBuildTask = new BaseBuildTask(this);

        _tasks[CollectorBotTaskName.MineralMining] = miningTask;
        _tasks[CollectorBotTaskName.BaseBuild] = baseBuildTask;

        _extractionState = new ExtractionState(miningTask);
        _flagPlaceState = new FlagPlaceState(miningTask);
        
        _currentState = _extractionState;
    }

    public void OnClick()
    {
        Click?.Invoke(this);
    }

    private void OnFlagInstalled(CollectorBotTaskName name)
    {
        if (_botDispatcher.AllCollectorsCount <= 1)
        {
            _flag.Deactivate();

            return;
        }

        _mainTask = _tasks[name];
        SwitchState(_flagPlaceState);
    }

    private void OnFlagDeactivated()
    {
        SwitchState(_extractionState);
    }
    
    private void SwitchState(BaseState state)
    {
        Debug.Log("SwitchState");

        _currentState.Exit();
        _currentState = state;
        _currentState.Entry(this);

        Debug.Log(_currentState);
    }

    private void ActivateScanner()
    {
        _scanner.Scan();
        _timer.Run();
    }
}