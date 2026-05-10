using UnityEngine;

public class Taker : MonoBehaviour
{
    [SerializeField] private Storage _storage;

    public bool IsStorageFilled => _storage.Item != null;

    public void PlaceResourceInStorage(IResource item)
    {
        _storage.SetItem(item);

        item.Transform.SetParent(_storage.transform);
        item.Transform.position = _storage.transform.position;

        item.Take();
    }
}
