public class BaseMenuService 
{
    private ResourceCounterViewer _resurceCounterViewer;
    private TimerViewer _timerViewer;
    private MenuActivator _menuActivator;

    public ResourceCounterViewer ResourceCounterViewer => _resurceCounterViewer;
    public TimerViewer TimerViewer => _timerViewer;
    public MenuActivator MenuActivator => _menuActivator;

    public BaseMenuService(ResourceCounterViewer resourceCounterViewer, TimerViewer timerViewer, MenuActivator menuActivator)
    {
        _resurceCounterViewer = resourceCounterViewer;
        _timerViewer = timerViewer;
        _menuActivator = menuActivator;
    }
}