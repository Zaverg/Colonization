using System;
using UnityEngine;

public class ResourceUnloader : Unloader
{
    [SerializeField] private Storage _storage;

    public override event Action<IResource> Unloaded;

    public override bool IsStorageEmpty => _storage.Item == null;

    public override IResource ReleaseResource()
    {
        IResource collectable = _storage.Item;
        collectable.Drop();
        ClearStorage();

        Unloaded?.Invoke(collectable);

        return collectable;
    }

    protected override void ClearStorage()
    {
        if (_storage == null)
            return;

        _storage.Item.Transform.SetParent(null);
        _storage.Clear();
    }
}