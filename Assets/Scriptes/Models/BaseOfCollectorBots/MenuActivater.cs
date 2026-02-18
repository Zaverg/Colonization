using UnityEngine;

public class MenuActivater : MonoBehaviour
{
    public IUiStats _current;

    public void SwitchActiveMenu(IUiStats stats)
    {
        if (stats == _current)
            return;

        _current.UnActivate();
        _current = stats;
        _current.Activate();
    }
}
