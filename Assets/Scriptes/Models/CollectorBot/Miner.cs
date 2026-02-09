using UnityEngine;

public abstract class Miner : MonoBehaviour, IMiner
{
    public abstract bool HasMined { get; }

    public abstract void SetDuration(float duration);

    public abstract void StartMining();
}
