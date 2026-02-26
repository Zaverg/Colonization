using UnityEngine;

public class CollectorBotFactory : MonoBehaviour
{
    private CollectorBot _prefab;
    private CoroutineRunner _coroutineRunner;

    public void Initialize(CollectorBot prefab, CoroutineRunner coroutineRunner)
    {
        _prefab = prefab;
        _coroutineRunner = coroutineRunner;
    }

    public CollectorBot Create()
    {
        CollectorBot bot = Instantiate(_prefab, transform.position, Quaternion.identity);
        bot.GetComponent<CollectorBotMiner>().SetCorutineRunner(_coroutineRunner);

        return bot;
    }
}