
public class CollectorBaseService
{
    private ICoroutineRunner _coroutineRunner;
    private MineralRegistry _mineralRegistry;
    private CollectorBotBaseConfig _config;
    private BaseMenu _baseMenu;
    private CollectorBotFactory _botFactory;
    private CollectorBotBaseFactory _baseFactory;

    public ICoroutineRunner CoroutineRunner => _coroutineRunner;
    public CollectorBotBaseConfig Config => _config;
    public MineralRegistry MineralRegistry => _mineralRegistry;
    public BaseMenu BaseMenu => _baseMenu;
    public CollectorBotFactory CollectorBotFactory => _botFactory;
    public CollectorBotBaseFactory BaseFactory => _baseFactory;

    public CollectorBaseService(ICoroutineRunner coroutineRunner, CollectorBotBaseConfig config, MineralRegistry mineralRegistry, 
        BaseMenu baseMenu, CollectorBotFactory botFactory, CollectorBotBaseFactory baseFactory)
    {
        _coroutineRunner = coroutineRunner;
        _config = config;
        _mineralRegistry = mineralRegistry;
        _baseMenu = baseMenu;
        _botFactory = botFactory;
        _baseFactory = baseFactory;
    }

    public ResourceCounter CreateResourceCounter() =>
        new ResourceCounter();
}
