using System;
using UnityEngine;

public class Unloader : MonoBehaviour
{
    [SerializeField] private Storage _storage;

    public event Action<IResource> Unloaded;

    public bool IsStorageEmpty => _storage.Item == null;

    public IResource ReleaseResource()
    {
        IResource resource = _storage.Item;
        resource.Drop();
        ClearStorage();

        Unloaded?.Invoke(resource);

        return resource;
    }

    private void ClearStorage()
    {
        if (_storage == null)
            return;

        _storage.Item.Transform.SetParent(null);
        _storage.Clear();
    }
}