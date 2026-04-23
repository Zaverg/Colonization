using System;
using UnityEngine;

public class ResourceUnloader : Unloader
{
    [SerializeField] private Storage _storage;

    public override event Action<IResource> Unloaded;

    public override bool IsStorageEmpty => _storage.Item == null;

    public override IResource ReleaseResource()
    {
        IResource resource = _storage.Item;
        resource.Drop();
        ClearStorage();

        Unloaded?.Invoke(resource);

        return resource;
    }

    protected override void ClearStorage()
    {
        if (_storage == null)
            return;

        _storage.Item.Transform.SetParent(null);
        _storage.Clear();
    }
}