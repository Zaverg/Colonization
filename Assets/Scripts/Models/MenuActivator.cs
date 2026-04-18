using UnityEngine;

public class MenuActivator
{
    public IMenu _current;

    public void SwitchActiveMenu(IMenu stats)
    {
        if (_current != null)
            _current.Deactivate();

        _current = null;

        if (stats != null)
        {
            _current = stats;
            _current.Activate();
        }
    }

    public void OnClosedMenu(Transform transform)
    {
        if (transform == null || transform.TryGetComponent<Map>(out _))
        {
            SwitchActiveMenu(null);
        }
    }
}
