using UnityEngine;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    [SerializeField] private List<CollectorBot> _collectors = new List<CollectorBot>();

    [SerializeField] private TimerViewer _scanIntervalView;
    
    [SerializeField] private CollectorBotDispatcher _collectorBotDispatcher;

    [SerializeField] private float _scanInterval;
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _scanLayer;

    private MineralRegistry _mineralRegistry;
    private Scanner _scanner;
    private ICoroutineRuner _coroutineRunner;
    private Timer _timer;

    private void OnEnable()
    {
        if (_timer == null)
            return;
        
        _timer.Ended += ActivateScanner;
        _timer.Changed += _scanIntervalView.UpdateView;
        _scanner.Detected += _mineralRegistry.Register;

        foreach (CollectorBot collector in _collectors)
            collector.OnBotAvailable += _collectorBotDispatcher.EnqueueCollector;
    }

    private void OnDisable()
    {
        if (_timer == null)
            return;

        _timer.Ended -= ActivateScanner;
        _timer.Changed -= _scanIntervalView.UpdateView;
        _scanner.Detected -= _mineralRegistry.Register;

        foreach (CollectorBot collector in _collectors)
            collector.OnBotAvailable -= _collectorBotDispatcher.EnqueueCollector;
    }

    private void Start()
    {
        _timer.Run();
    }

    private void Update()
    {
        if (_collectorBotDispatcher.AvailableCollectorsCount == 0)
            return;

        CollectorBot collector = _collectorBotDispatcher.GetAvailableCollectorBot();

        AssignMiningTask(collector);
    }

    public void Initialize(ICoroutineRuner coroutineRuner, MineralRegistry mineralRegistry)
    {
        _mineralRegistry = mineralRegistry;
        _coroutineRunner = coroutineRuner;

        _scanner = new Scanner(transform.position, _scanLayer, _scanRadius);

        _timer = new Timer(_coroutineRunner);
        _timer.SetDuration(_scanInterval);

        _collectorBotDispatcher.Initialize(_collectors);

        gameObject.SetActive(true);
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

    private void ActivateScanner()
    {
        _scanner.Scan();
        _timer.Run();
    }
}
