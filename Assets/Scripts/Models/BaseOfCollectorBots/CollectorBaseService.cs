
public class CollectorBaseService
{
    private ICoroutineRunner _coroutineRunner;
    private CollectorBotBaseConfig _config;

    public ICoroutineRunner CoroutineRunner => _coroutineRunner;
    public CollectorBotBaseConfig Config => _config;

    public CollectorBaseService(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public ResourceCounter GetResourceCounter() =>
        new ResourceCounter();
}
