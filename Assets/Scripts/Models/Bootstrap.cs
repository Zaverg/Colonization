using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CollectorBot _prefab;
    [SerializeField] private CollectorBotBase _base;
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private CollectorBotFactory _fabricCollectorBot;

    [SerializeField] private SpawnGrid _cellRegister;
    [SerializeField] private ObjectPoolMineral _objectPullMineral;
    [SerializeField] private Map _map;

    [SerializeField] private MineralRegistry _mineralRegistry;

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BaseStats _baseMenu;
    [SerializeField] private BaseMenuViwer _baseMenuViewer;
    [SerializeField] private FlagSpawner _flagSpawner;

    private void Awake()
    {
        _cellRegister.gameObject.SetActive(false);
        _mineralSpawner.gameObject.SetActive(false);
        _base.gameObject.SetActive(false);
        _inputReader.gameObject.SetActive(false);

        _inputReader.Initialize();

        _map.Initialize();
        _objectPullMineral.Initialize();
        _cellRegister.Initialize();

        _mineralSpawner.Initialize(_coroutineRunner, _mineralRegistry);

        _fabricCollectorBot.Initialize(_prefab, _coroutineRunner);

        //_base.Initialize(_coroutineRunner, _mineralRegistry, _fabricCollectorBot);    
    }

    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
        
    }
}
