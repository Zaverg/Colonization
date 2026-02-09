using UnityEngine;

public abstract class Taker : MonoBehaviour, ITaker
{
    public abstract bool IsStorageFilled { get; }

    public abstract void PlaceResourceInStorage(IResource collectable);
}