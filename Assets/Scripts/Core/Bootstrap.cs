using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MapInitializer _mapInitializer;
    [SerializeField] private BuildInitialize _buildInitialize;
    [SerializeField] private UiInitializer _uiInitializer;

    [SerializeField] private CoroutineRunner _coroutineRunner;

    private bool _isInitialized = false;
    
    private void Awake()
    {
        _mapInitializer.Initialize(_coroutineRunner);
        _uiInitializer.Initialize();

        _buildInitialize.Initialize(_coroutineRunner);

        _isInitialized = true;
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
            return;

        _buildInitialize.BotHubFactory.Created += _uiInitializer.OnBaseCreated;
        _uiInitializer.Subscribe();
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            return;

        _buildInitialize.BotHubFactory.Created -= _uiInitializer.OnBaseCreated;
        _uiInitializer.Unsubscribe();
    }

    public void Start()
    {
        BuildProcess buildProcess = _buildInitialize.BuildProcessSpawner.Create(BuildType.CollectorBase);
        buildProcess.transform.position = _mapInitializer.Map.transform.position;
        List<Vector2Int> gridPosition = _mapInitializer.CellRegister.TryGetOccupyArea(buildProcess.CalculateArea());

        buildProcess.Install(gridPosition);

        BotHub botHub = _buildInitialize.BotHubFactory.Create(_mapInitializer.Map.transform.position, gridPosition) as BotHub;

        buildProcess.Release();

        for (int i = 0; i < _buildInitialize.CountStartBot; i++)
        {
            CollectorBot bot = _buildInitialize.CollectorBotFactory.Create(botHub.SpawnBotPlace.position);
            botHub.BotDispatcher.EnqueueBot(bot);
        }
    }
}
