using System;

public class BaseMenu : IUiStats
{
    private ICollectorBase _collectorBase;

    private ResourceCounterViewer _resourceCountViewer;
    private TimerViewer _timerViewer;
    private BaseMenuViewer _baseMenuViewer;
    private BaseBuildButton _flagButton;

    public event Action<IUiStats> OnActiveChanged;

    public ICollectorBase CurrentBase => _collectorBase;

    public BaseMenu(TimerViewer timerViewer, ResourceCounterViewer resourceCounterViewer, BaseMenuViewer baseMenuViewer, BaseBuildButton flagButton)
    {
        _timerViewer = timerViewer;
        _resourceCountViewer = resourceCounterViewer;
        _baseMenuViewer = baseMenuViewer;
        _flagButton = flagButton;
    }

    public void Show(ICollectorBase collectorBase)
    { 
        _collectorBase = collectorBase;

        OnActiveChanged?.Invoke(this);

        collectorBase.Click -= Show;
    }

    public void Activate()
    {
        _collectorBase.ResourceCounter.MineralCountChanged += _resourceCountViewer.UpdateView;
        _collectorBase.Timer.Changed += _timerViewer.UpdateView;
        _flagButton.FlagActivated += _collectorBase.Flag.OnButtonClick;

        _baseMenuViewer.gameObject.SetActive(true);

        _resourceCountViewer.UpdateView(_collectorBase.ResourceCounter.CollectedResources);
        _timerViewer.UpdateView(_collectorBase.Timer.CurrentSeconds);
    }

    public void Deactivate()
    {
        _collectorBase.ResourceCounter.MineralCountChanged -= _resourceCountViewer.UpdateView;
        _collectorBase.Timer.Changed -= _timerViewer.UpdateView;
        _flagButton.FlagActivated -= _collectorBase.Flag.OnButtonClick;

        _baseMenuViewer.gameObject.SetActive(false);
    }
}