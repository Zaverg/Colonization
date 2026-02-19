using UnityEngine;
using System.Collections.Generic;
using System;

public class CollectorBotBase : MonoBehaviour, IClickeble
{
    [SerializeField] private List<CollectorBot> _collectors = new List<CollectorBot>();

    [SerializeField] private int _countResurceToCreateBot;

    private CollectorBotDispatcher _collectorBotDispatcher;
    private CollectorBotBaseConfig _config;
    private MineralRegistry _mineralRegistry;
    private Scanner _scanner;
    private CollectorBotFactory _fabricCollectorBot;
    private BaseStats _baseStats;

    private void OnEnable()
    {
        if (_baseStats == null)
            return;

        _baseStats.Timer.Ended += ActivateScanner;
        _scanner.Detected += _mineralRegistry.Register;
        _baseStats.ResurceCounter.MineralCountChanged += OnValidateCountResurce;

        foreach (CollectorBot collector in _collectors)
        {
            collector.OnBotAvailable += _collectorBotDispatcher.EnqueueCollector;
            //collector.Unloader.Unloaded += _resurceCounter.UpdateCounter;
        }
    }

    private void OnDisable()
    {
        if (_baseStats == null)
            return;

        _baseStats.Timer.Ended -= ActivateScanner;
        _scanner.Detected -= _mineralRegistry.Register;
        _baseStats.ResurceCounter.MineralCountChanged -= OnValidateCountResurce;

        foreach (CollectorBot collector in _collectors)
        {
            collector.OnBotAvailable -= _collectorBotDispatcher.EnqueueCollector;
            //collector.Unloader.Unloaded -= _resurceCounter.UpdateCounter;
        }
    }

    private void Start()
    {
        _baseStats.Timer.Run();
    }

    private void Update()
    {
        if (_collectorBotDispatcher.AvailableCollectorsCount == 0)
            return;

        CollectorBot collector = _collectorBotDispatcher.GetAvailableCollectorBot();

        //AssignMiningTask(collector);
    }

    public void Initialize(Timer timer, BaseStats baseStats, CollectorBotBaseConfig config)
    {
        _baseStats = baseStats;
        _config = config;

        _scanner = new Scanner(transform.position, _config.ScanLayer, _config.ScanRadius);

        timer.SetDuration(_config.ScanInterval);

        _collectorBotDispatcher = new CollectorBotDispatcher();

        gameObject.SetActive(true);
    }

    public void OnClick()
    {

    }

    public void OnValidateCountResurce(int count)
    {
        if (count != 0 && count % _countResurceToCreateBot == 0)
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
        _baseStats.Timer.Run();
    }
}

public abstract class CollectorBotBaseState : State
{
    public abstract void Entry(IBase collectorBase);
}

public class MiningTaskState : CollectorBotBaseState
{
    public override event Action Completed;

    public override void Entry()
    {
        throw new NotImplementedException();
    }

    public override void Entry(IBase stateMachine)
    {
        throw new NotImplementedException();
    }

    public override void Exit()
    {
        throw new NotImplementedException();
    }

    public override void Run()
    {
        throw new NotImplementedException();
    }
}

public class MiningTask
{
    private MineralRegistry _mineralRegistry;
    private CollectorBotDispatcher _collectorBotDispatcher;
    private CoroutineRunner _coroutineRunner;

    public MiningTask()
    {

    }
    
    private void AssignMiningTask(CollectorBot collector)
    {
        if (_mineralRegistry.AvailableMineralsCount == 0)
        {
            _collectorBotDispatcher.EnqueueCollector(collector);

            return;
        }

        IResource mineral = _mineralRegistry.GetAvailableMineral();

        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, mineral.Transform.position));
        tasks.Enqueue(new CollectorBotTask(StateType.Mining, mineral: mineral, coroutineStarter: _coroutineRunner));
        tasks.Enqueue(new CollectorBotTask(StateType.Taking, mineral: mineral));
        tasks.Enqueue(new CollectorBotTask(StateType.Moving, collector.transform.position));
        tasks.Enqueue(new CollectorBotTask(StateType.Dropping));

        collector.AssignTasks(tasks);
    }
}

public interface IBase
{

}