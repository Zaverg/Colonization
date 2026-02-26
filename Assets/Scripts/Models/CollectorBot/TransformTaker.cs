using UnityEngine;

public class TransformTaker : Taker
{
    [SerializeField] private Storage _storage;

    public override bool IsStorageFilled => _storage.Item != null;

    public override void PlaceResourceInStorage(IResource item)
    {
        _storage.SetItem(item);

        item.Transform.SetParent(_storage.transform);
        item.Transform.position = _storage.transform.position;

        item.Take();
    }
}
