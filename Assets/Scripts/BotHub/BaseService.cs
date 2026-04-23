
public class BaseService
{
    private ICoroutineRunner _coroutineRunner;
    private MineralRegistry _mineralRegistry;
    private CollectorBotBaseConfig _config;
    private BaseMenu _baseMenu;
    private CollectorBotSpawner _botSpawner;
    private BotHubFactory _baseFactory;
    private BuildProcessPool _buildProcessPool;

    public ICoroutineRunner CoroutineRunner => _coroutineRunner;
    public CollectorBotBaseConfig Config => _config;
    public MineralRegistry MineralRegistry => _mineralRegistry;
    public BaseMenu BaseMenu => _baseMenu;
    public CollectorBotSpawner CollectorBotSpawner => _botSpawner;
    public BotHubFactory BaseFactory => _baseFactory;
    public BuildProcessPool BuildProcessPool => _buildProcessPool;

    public BaseService(ICoroutineRunner coroutineRunner, CollectorBotBaseConfig config, MineralRegistry mineralRegistry, 
        BaseMenu baseMenu, CollectorBotSpawner botFactory, BotHubFactory baseFactory, BuildProcessPool buildProcessPool)
    {
        _coroutineRunner = coroutineRunner;
        _config = config;
        _mineralRegistry = mineralRegistry;
        _baseMenu = baseMenu;
        _botSpawner = botFactory;
        _baseFactory = baseFactory;
        _buildProcessPool = buildProcessPool;
    }

    public ResourceCounter CreateResourceCounter() =>
        new ResourceCounter();
}
