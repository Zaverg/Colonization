using UnityEngine;

public class Storage : MonoBehaviour
{
    private IResource _item;

    public IResource Item => _item;

    public void SetItem(IResource collectable)
    {
        if (collectable == null)
            return;

        _item = collectable;
    }

    public void Clear()
    {
        if (_item == null)
            return;

        _item = null;
    }
}