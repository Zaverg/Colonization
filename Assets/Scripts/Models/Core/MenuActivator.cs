using UnityEngine;

public class MenuActivator
{
    private IMenu _current;

    public void SwitchActiveMenu(IMenu menu)
    {
        if (_current != null)
            _current.Deactivate();

        _current = null;

        if (menu != null)
        {
            _current = menu;
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
