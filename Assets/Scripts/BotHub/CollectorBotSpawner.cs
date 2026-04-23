using UnityEngine;

public class CollectorBotSpawner : MonoBehaviour
{
    private CollectorBot _prefab;
    private CoroutineRunner _coroutineRunner;

    public void Initialize(CollectorBot prefab, CoroutineRunner coroutineRunner)
    {
        _prefab = prefab;
        _coroutineRunner = coroutineRunner;
    }

    public CollectorBot Spawn(Vector3 position, bool startActive)
    {
        CollectorBot bot = Instantiate(_prefab, position, Quaternion.identity);
        bot.GetComponent<CollectorBotMiner>().SetCoroutineRunner(_coroutineRunner);

        bot.gameObject.SetActive(startActive);

        return bot;
    }
}