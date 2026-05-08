using UnityEngine;

public class BuildInitialize : MonoBehaviour
{
    [SerializeField] private CollectorBotFactory _collectorBotFactory;
    [SerializeField] private BuildProcessFactory _buildProcessFactory;
    [SerializeField] private BotHubFactory _botHubFactory;
    [SerializeField] private BuildProcessPool _buildProcessPool;
    [SerializeField] private BuildProcessPlacer _buildProcessPlacer;
    [SerializeField] private int _countStartBot = 3;

    public BuildProcessFactory BuildProcessSpawner => _buildProcessFactory;
    public BotHubFactory BotHubFactory => _botHubFactory;
    public CollectorBotFactory CollectorBotFactory => _collectorBotFactory;
    public int CountStartBot => _countStartBot;

    public void Initialize(CoroutineRunner coroutineRunner)
    {
        _buildProcessPool.Initialize();
    }
}