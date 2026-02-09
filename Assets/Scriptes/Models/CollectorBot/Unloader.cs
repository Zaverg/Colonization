using UnityEngine;

public abstract class Unloader : MonoBehaviour, IUnloader
{
    public abstract bool IsStorageEmpty { get; }
    public abstract IResource ReleaseResource();
    protected abstract void ClearStorage();
}
