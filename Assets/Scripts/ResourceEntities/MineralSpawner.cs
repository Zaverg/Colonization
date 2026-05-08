using System;
using UnityEngine;
using System.Collections;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private CellRegister _spawnGrid;

    [SerializeField, Range(0, 5)] private int _maxMinerals;

    [SerializeField] private ObjectPoolMineral _mineralPool;

    [SerializeField] private float _spawnInterval;
    [SerializeField] private TimerViewer _timerView;

    [SerializeField] private MineralRegistry _mineralRegistry;

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

    public void Initialize(CoroutineRunner coroutineRunner)
    {
        _timer = new Timer(coroutineRunner);
        _timer.SetDuration(_spawnInterval);

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

            _spawnGrid.OccupyRandomCell(mineral);
            _mineralRegistry.Register(mineral);

            _currentCount++;
            
            yield return null;
        }

        _coroutine = null;
        _timer.Run();
    }
}