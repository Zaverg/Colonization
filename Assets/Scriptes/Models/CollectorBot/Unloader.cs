using UnityEngine;
using System;

public abstract class Unloader : MonoBehaviour, IUnloader
{
    public abstract event Action<IResource> Unloaded;

    public abstract bool IsStorageEmpty { get; }
    public abstract IResource ReleaseResource();
    protected abstract void ClearStorage();
}
