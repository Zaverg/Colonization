using System;

public class BaseMenu : IUiStats
{
    private ICollectorBase _collectorBase;
    private MenuActivator _menuActivator;

    private ResourceCounterViewer _resurceCountViewer;
    private TimerViewer _timerViewer;

    public event Action<IUiStats> OnActiveChanged;

    public BaseMenu(TimerViewer timerViewer, ResourceCounterViewer resurceCounterViewer, MenuActivator menuActivator)
    {
        _timerViewer = timerViewer;
        _resurceCountViewer = resurceCounterViewer;
        _menuActivator = menuActivator;
    }

    public void Show(ICollectorBase collectorBase)
    {
        _collectorBase = collectorBase;

        OnActiveChanged?.Invoke(this);
    }

    public void Activate()
    {
        _collectorBase.ResurceCounter.MineralCountChanged += _resurceCountViewer.UpdateView;
        _collectorBase.Timer.Changed += _timerViewer.UpdateView;

        _resurceCountViewer.UpdateView(_collectorBase.ResurceCounter.CollectedResources);
        _timerViewer.UpdateView(_collectorBase.Timer.CurrentSeconds);
    }

    public void Deactivate()
    {
        _collectorBase.ResurceCounter.MineralCountChanged -= _resurceCountViewer.UpdateView;
        _collectorBase.Timer.Changed -= _timerViewer.UpdateView;
    }
}