using System;
using UnityEngine;

public class BotHubMenu : MonoBehaviour, IMenu
{
    [SerializeField] private BuildProcessPlacer _buildProcessPlacer;
    [SerializeField] private BuildProcessFactory _buildProcessFactory;

    [SerializeField] private BotHubMenuViewer _menuViewer;
    [SerializeField] private MenuAnimation _menuAnimation;
    [SerializeField] private float _durationAnimation;

    private IBotHub _botHub;

    public event Action<IMenu> Activated;

    public IBotHub CurrentBase => _botHub;

    public void Show(ClickableObject clickableObject)
    { 
        _botHub = clickableObject.GetComponent<BotHub>();

        if (_botHub.Flag.BuildProcess != null && _botHub.Flag.BuildProcess.gameObject.activeSelf || _botHub.BotDispatcher.AllBotsCount <= 1)
            _menuViewer.BotHubBuildButton.SetActive(false);
        else
            _menuViewer.BotHubBuildButton.SetActive(true);

        Activated?.Invoke(this);
    }

    public void Activate()
    {
        _botHub.ResourceCounter.CountChanged += _menuViewer.Resource.UpdateView;
        _botHub.Scanner.Timer.Changed += _menuViewer.TimerViewer.UpdateView;
        _botHub.BotDispatcher.CountChanged += _menuViewer.AllCollectorBots.UpdateView;

        _menuViewer.BotHubBuildButton.OnBuild += _buildProcessPlacer.StartPlacement;

        _menuAnimation.OpenMenu(_durationAnimation);

        _menuViewer.Resource.UpdateView(_botHub.ResourceCounter.CollectedResources);
        _menuViewer.TimerViewer.UpdateView(_botHub.Scanner.Timer.CurrentSeconds);
        _menuViewer.AllCollectorBots.UpdateView(_botHub.BotDispatcher.AllBotsCount);
    }

    public void Deactivate()
    {
        _botHub.ResourceCounter.CountChanged -= _menuViewer.Resource.UpdateView;
        _botHub.Scanner.Timer.Changed -= _menuViewer.TimerViewer.UpdateView;
        _botHub.BotDispatcher.CountChanged -= _menuViewer.AllCollectorBots.UpdateView;

        _menuViewer.BotHubBuildButton.OnBuild -= _buildProcessPlacer.StartPlacement;

        _menuAnimation.CloseMenu(_durationAnimation);
    } 
}
