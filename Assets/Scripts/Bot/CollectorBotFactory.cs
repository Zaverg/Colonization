using UnityEngine;

public class CollectorBotFactory : MonoBehaviour
{
    [SerializeField] private CollectorBot _prefab;
    [SerializeField] private CoroutineRunner _coroutineRunner;

    public CollectorBot Create(Vector3 position)
    {
        CollectorBot bot = Instantiate(_prefab, position, Quaternion.identity);
        bot.GetComponent<Miner>().SetCoroutineRunner(_coroutineRunner);

        return bot;
    }
}