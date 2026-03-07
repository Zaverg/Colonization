public class MenuActivator
{
    public IUiStats _current;

    public void SwitchActiveMenu(IUiStats stats)
    {
        if (stats == _current)
            return;

        if (_current != null)
            _current.Deactivate();

        if (stats == null)
            return;

        _current = stats;
        _current.Activate();
    }
}
