using System;

public class BaseStats : IUiStats
{
    private Timer _timer;
    private ResurceCounter _resurceCounter;

    private ResurceCounterViewer _resurceCountViewer;
    private TimerViewer _timerViewer;

    public event Action<IUiStats> OnActiveChanged;

    public Timer Timer => _timer;
    public ResurceCounter ResurceCounter => _resurceCounter;

    public BaseStats(Timer timer, ResurceCounter resurceCounter, TimerViewer timerViewer, ResurceCounterViewer resurceCounterViewer)
    {
        _timer = timer;
        _timerViewer = timerViewer;
        _resurceCounter = resurceCounter;
        _resurceCountViewer = resurceCounterViewer;
    }

    public void Show()
    {
        OnActiveChanged?.Invoke(this);
    }

    public void Activate()
    {
        _resurceCounter.MineralCountChanged += _resurceCountViewer.UpdateView;
        _timer.Changed += _timerViewer.UpdateView;

        _resurceCountViewer.UpdateView(_resurceCounter.CollectedResources);
        _timerViewer.UpdateView(_timer.CurrentSeconds);
    }

    public void UnActivate()
    {
        _resurceCounter.MineralCountChanged -= _resurceCountViewer.UpdateView;
        _timer.Changed -= _timerViewer.UpdateView;
    }
}