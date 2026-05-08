using System;
using System.Collections.Generic;
using UnityEngine;

public class BotHub : Building, IBotHub
{
    [SerializeField] private Flag _flag;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Transform _spawnBotPlace;

    private BaseState _currentState;
    private Dictionary<Type, BaseState> _states = new Dictionary<Type, BaseState>();

    private BotDispatcher _botDispatcher;
    private ResourceCounter _resourceCounter;
    private MineralRegistry _mineralRegistry;
    private PriceList _priceList;

    public event Action<BotHub> Disabled;

    public MineralRegistry MineralRegistry => _mineralRegistry;
    public PriceList PriceList => _priceList;
    public Scanner Scanner => _scanner;
    public ResourceCounter ResourceCounter => _resourceCounter;
    public BotDispatcher BotDispatcher => _botDispatcher;
    public Flag Flag => _flag;
    public Transform SpawnBotPlace => _spawnBotPlace;


    private void OnEnable()
    {
        if (_mineralRegistry == null)
            return;

        _flag.Installed += OnFlagInstalled;
        _flag.Deactivated += OnFlagDeactivated;
        _scanner.Detected += _mineralRegistry.Register;
    }

    private void OnDisable()
    {
        if (_mineralRegistry == null)
            return;

        _flag.Installed -= OnFlagInstalled;
        _flag.Deactivated -= OnFlagDeactivated;
        _scanner.Detected -= _mineralRegistry.Register;

        Disabled?.Invoke(this);
    }

    private void Start()
    {
        _currentState.Entry(this);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        _currentState.Run();
    }

    public void Initialize(MineralRegistry mineralRegister, CollectorBotFactory collectorBotFactory, CoroutineRunner coroutineRunner, PriceList priceList)
    {
        _resourceCounter = new ResourceCounter();
        _botDispatcher = new BotDispatcher(_resourceCounter);

        Timer timer = new Timer(coroutineRunner);
        _scanner.Initialize(timer);

        _priceList = priceList;
        _mineralRegistry = mineralRegister;

        MiningTask miningTask = new MiningTask(_mineralRegistry, coroutineRunner, transform.position);
        BuildTask buildTask = new BuildTask(this);

        DefaultState defaultState = new DefaultState(miningTask, collectorBotFactory);
        FlagPlaceState flagPlaceState = new FlagPlaceState(buildTask, miningTask);

        _states[typeof(DefaultState)] = defaultState;
        _states[typeof(FlagPlaceState)] = flagPlaceState;

        _currentState = defaultState;
    }

    private void OnFlagInstalled()
    {
        SwitchState(typeof(FlagPlaceState));
    }

    private void OnFlagDeactivated()
    {
        SwitchState(typeof(DefaultState));
    }
    
    private void SwitchState(Type typeState)
    {
        Debug.Log("SwitchState");

        _currentState.Exit();
        _currentState = _states[typeState];
        _currentState.Entry(this);
    }
}