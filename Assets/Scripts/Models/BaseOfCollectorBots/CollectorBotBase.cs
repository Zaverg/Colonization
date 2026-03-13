using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectorBotBase : MonoBehaviour, IClickable, ICollectorBase, ICreatable
{
    [SerializeField] private int _countResourceToCreateBot = 3;
    [SerializeField] private int _countResourceToBuildBase = 5;
    [SerializeField] private Flag _flag;
    [SerializeField] private Transform _spawnBotPlace;

    private float _scanTime = 5;

    private CollectorBaseState _currentState;
    private ExtractionState _extractionState;
    private FlagPlaceState _flagPlaceState;

    private Dictionary<CollectorBotTaskName, CollectorBaseTask> _tasks = new Dictionary<CollectorBotTaskName, CollectorBaseTask>();
    private CollectorBaseTask _mainTask;

    private CollectorBotDispatcher _collectorBotDispatcher;
    private CollectorBotBaseConfig _config;
    private MineralRegistry _mineralRegistry;
    private Scanner _scanner;
    private CollectorBotFactory _factoryCollectorBot;
    private Timer _timer;
    private ResourceCounter _resourceCounter;

    public event Action<ICollectorBase> Click;
    public event Action<IBuild> OnEndBuild;
    public event Action<ICollectorBase> Disabled;

    public Timer Timer => _timer;
    public ResourceCounter ResourceCounter => _resourceCounter;
    public int CountResourceToCreateBot => _countResourceToCreateBot;
    public int CountResourceToBuildBase => _countResourceToBuildBase;
    public CollectorBotDispatcher BotDispatcher => _collectorBotDispatcher;
    public Flag Flag => _flag;
    public MineralRegistry MineralRegistry => _mineralRegistry;
    public IFactory FactoryBot => _factoryCollectorBot;
    public Transform SpawnBotPlace => _spawnBotPlace;
    public CollectorBaseTask MainTask => _mainTask;

    private void OnEnable()
    {
        _timer.Ended += ActivateScanner;
        _scanner.Detected += _mineralRegistry.Register;

        if (_flag == null)
            return;

        _flag.Installed += PlaceFlag;
        _flag.Deactivated += TakeFlag;
    }

    private void OnDisable()
    {
        _timer.Ended -= ActivateScanner;
        _scanner.Detected -= _mineralRegistry.Register;

        if (_flag == null)
            return;

        _flag.Installed -= PlaceFlag;
        _flag.Deactivated -= TakeFlag;

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

    public void Initialize(CollectorBaseService collectorBaseService)
    {
        _config = collectorBaseService.Config;
        _mineralRegistry = collectorBaseService.MineralRegistry;
        _resourceCounter = new ResourceCounter();

        _scanner = new Scanner(transform.position, _config.ScanLayer, _config.ScanRadius);

        _timer = new Timer(collectorBaseService.CoroutineRunner);
        _timer.SetDuration(_scanTime);

        _factoryCollectorBot = collectorBaseService.CollectorBotFactory;
        _collectorBotDispatcher = new CollectorBotDispatcher(_resourceCounter);

        MiningTask miningTask = new MiningTask(_mineralRegistry, _collectorBotDispatcher, collectorBaseService.CoroutineRunner, transform.position);
        BaseBuildTask baseBuildTask = new BaseBuildTask(this, collectorBaseService.BaseFactory, collectorBaseService.BuildProcessPool, collectorBaseService.CoroutineRunner);

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

    private void PlaceFlag(CollectorBotTaskName name)
    {
        Debug.Log(_collectorBotDispatcher.AllCollectorsCount);
        if (_collectorBotDispatcher.AllCollectorsCount <= 1)
        {
            _flag.Deactivate();

            return;
        }

        _mainTask = _tasks[name];
        SwitchState(_flagPlaceState);
    }

    private void TakeFlag()
    {
        SwitchState(_extractionState);
    }
    
    private void SwitchState(CollectorBaseState state)
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