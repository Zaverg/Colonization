using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

public class CollectorBotBase : MonoBehaviour, IClickable, ICollectorBase
{
    [SerializeField] private List<CollectorBot> _collectors = new List<CollectorBot>();
    [SerializeField] private int _countResourceToCreateBot;

    private CollectorBaseTask _currentTask;

    private MiningTask _miningTask;
    private BuildTask _buildTask;

    private CollectorBotDispatcher _collectorBotDispatcher;
    private CollectorBotBaseConfig _config;
    private MineralRegistry _mineralRegistry;
    private Scanner _scanner;
    private BaseStats _baseStats;
    private CollectorBotFactory _fabricCollectorBot;

    private void OnEnable()
    {
        if (_baseStats == null)
            return;

        _baseStats.Timer.Ended += ActivateScanner;
        _scanner.Detected += _mineralRegistry.Register;
        _baseStats.ResurceCounter.MineralCountChanged += OnValidateCountResource;

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
        _baseStats.ResurceCounter.MineralCountChanged -= OnValidateCountResource;

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

        _currentTask.AssignTask(collector);
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

    public void SwitchToBildTask(Flag flag)
    {
        _currentTask = _buildTask;
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
        _baseStats.Timer.Run();
    }
}

public abstract class CollectorBaseState
{
    public abstract event Action Completed;

    public abstract void Entry();
    public abstract void Run();
    public abstract void Exit();
}

public class ExtractiveState : CollectorBaseState
{
    public MiningTask _miningTask;
    public 

    public override event Action Completed;

    public override void Entry()
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

public class CollectorBotBuilderTask : CollectorBaseTask
{
    public override void AssignTask(CollectorBot collector)
    {
      
    }
}