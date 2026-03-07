using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private Flag _prefab;

    public void Spawn(ICollectorBase collectorBase)
    {
        Flag flag = Instantiate(_prefab);
        flag.gameObject.SetActive(false);

        collectorBase.SetFlag(flag);
    }
}