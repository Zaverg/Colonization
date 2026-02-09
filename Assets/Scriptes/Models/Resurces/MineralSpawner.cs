using System;
using UnityEngine;
using System.Collections;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private SpawnGrid _spawnGrid;

    [SerializeField, Range(0, 5)] private int _maxMinerals;

    [SerializeField] private ObjectPoolMineral _mineralPool;

    [SerializeField] private float _spawnInterval;
    [SerializeField] private TimerViewer _timerView;

    private MineralRegistry _mineralRegistry;
    private ResurceCounter _resurceCounter;

    private Timer _timer;
    private int _currentCount;
    private Coroutine _coroutine;
 
    private void OnEnable()
    {
        if (_timer == null)
            return;

        _timer.Ended += StartSpawning;
        _timer.Changed += _timerView.UpdateView;
    }

    private void OnDisable()
    {
        if (_timer == null)
            return;

        _timer.Ended -= StartSpawning;
        _timer.Changed -= _timerView.UpdateView;
    }

    private void Start()
    {
        StartSpawning();
    }

    public void Initialize(ICoroutineRuner coroutineRuner, MineralRegistry mineralRegistry, ResurceCounter resurceCounter)
    {
        _timer = new Timer(coroutineRuner);
        _timer.SetDuration(_spawnInterval);

        _mineralRegistry = mineralRegistry;
        _resurceCounter = resurceCounter;

        gameObject.SetActive(true);
    }

    private void StartSpawning()
    {
        if (_coroutine != null)
            return;

        _currentCount = _mineralRegistry.AvailableMineralsCount;
        _coroutine = StartCoroutine(Spawn());
    }
    
    private IEnumerator Spawn()
    {
        int maxIndex = Enum.GetValues(typeof(MineralType)).Length;

        while (_currentCount < _maxMinerals)
        {
            int indexType = UnityEngine.Random.Range(0, maxIndex);
            MineralType type = (MineralType)indexType;

            Mineral mineral = _mineralPool.GetMineral(type);
            mineral.Dropped += _resurceCounter.UpdateCounter;

            _spawnGrid.OccupyCell(mineral);

            _currentCount++;
            
            yield return null;
        }

        _coroutine = null;
        _timer.Run();
    }
}