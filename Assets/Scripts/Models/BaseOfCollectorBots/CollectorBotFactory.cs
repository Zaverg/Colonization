using UnityEngine;

public class CollectorBotFactory : MonoBehaviour, IFactory
{
    private CollectorBot _prefab;
    private CoroutineRunner _coroutineRunner;

    public void Initialize(CollectorBot prefab, CoroutineRunner coroutineRunner)
    {
        _prefab = prefab;
        _coroutineRunner = coroutineRunner;
    }

    public ICreatable Create(Vector3 position, bool visible)
    {
        CollectorBot bot = Instantiate(_prefab, position, Quaternion.identity);
        bot.GetComponent<CollectorBotMiner>().SetCorutineRunner(_coroutineRunner);

        bot.gameObject.SetActive(visible);

        return bot;
    }
}