public class MenuActivator
{
    public IUiStats _current;

    public void SwitchActiveMenu(IUiStats stats)
    {
        if (stats == _current)
            return;

        _current.Deactivate();
        _current = stats;

        if (stats == null)
            return;

        _current.Activate();
    }
}
