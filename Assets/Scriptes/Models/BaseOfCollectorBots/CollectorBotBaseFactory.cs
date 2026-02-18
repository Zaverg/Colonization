using UnityEngine;

public class CollectorBotBaseFactory : MonoBehaviour
{
    [SerializeField] private CollectorBotBase _base;
    [SerializeField] private CollectorBotBaseConfig _config;

    private ICoroutineRuner _coroutineRuner;
    private ResurceCounterViewer _resurceCounterViewer;
    private TimerViewer _timerViewer;
    private CollectorBotFactory _collectorBotFactory;

    public void Initialize(ICoroutineRuner coroutineRuner, CollectorBotFactory collectorBotFactory, 
        ResurceCounterViewer resurceCounterViewer, TimerViewer timerViewer)
    {
        _coroutineRuner = coroutineRuner;
        _resurceCounterViewer = resurceCounterViewer;
        _timerViewer = timerViewer;
    }

    public CollectorBotBase Create(Vector3 position)
    {
        Timer timer = new Timer(_coroutineRuner);
        ResurceCounter resurcCounter = new ResurceCounter();
        BaseStats baseStats = new BaseStats(timer, resurcCounter, _timerViewer, _resurceCounterViewer);

        CollectorBotBase collectorBotBase = Instantiate(_base, position, Quaternion.identity);
        collectorBotBase.Initialize(timer, baseStats, _config);

        return collectorBotBase;
    }
}