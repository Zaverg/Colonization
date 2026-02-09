using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private CoroutineRunner _coroutineStarter;

    [SerializeField] private SpawnGrid _cellRegister;
    [SerializeField] private ObjectPoolMineral _objectPullMineral;
    [SerializeField] private Map _map;
    [SerializeField] private ResurceCounter _reesurceCounter;

    [SerializeField] private MineralRegistry _mineralRegistry;
    [SerializeField] private CollectorBotDispatcher _collectorBotDispatcher;

    [SerializeField] private MineralCountViewer _mineralCountView;

    private void Awake()
    {
        _cellRegister.gameObject.SetActive(false);
        _mineralSpawner.gameObject.SetActive(false);
        _base.gameObject.SetActive(false);

        _map.Initialize();
        _objectPullMineral.Initialize();
        _cellRegister.Initialize();

        _mineralSpawner.Initialize(_coroutineStarter, _mineralRegistry, _reesurceCounter);

        _base.Initialize(_coroutineStarter, _mineralRegistry);        
    }

    private void OnEnable()
    {
        _reesurceCounter.MineralCountChanged += _mineralCountView.UpdateView;
    }

    private void OnDisable()
    {
        _reesurceCounter.MineralCountChanged -= _mineralCountView.UpdateView;
    }
}
