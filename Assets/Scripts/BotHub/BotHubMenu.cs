using UnityEngine;

public class BotHubMenu : MonoBehaviour
{
    [SerializeField] private BotHub _botHub;
    [SerializeField] private BotHubBuildButton _botHubBuildButton;
    [SerializeField] private CounterViewer _allCollectorBots;
    [SerializeField] private CounterViewer _resourceCounter;
    [SerializeField] private BuildProcessPlacer _buildProcessPlacer;

    public void OnDisable()
    {
        _botHub.BotDispatcher.CountChanged -= _allCollectorBots.UpdateView;
        _botHub.ResourceCounter.CountChanged -= _resourceCounter.UpdateView;

        _botHubBuildButton.Clicked -= OnClickBuildBotHubButton;
    }

    public void Update()
    {
        if (_botHubBuildButton.gameObject.activeSelf)
        {
            if (_botHub.Flag.BuildProcess != null && _botHub.BotDispatcher.AllBotsCount <= 1)
                _botHubBuildButton.SetActive(false);
        }
        else
        {
            if (_botHub.Flag.BuildProcess == null && _botHub.BotDispatcher.AllBotsCount > 1)
                _botHubBuildButton.SetActive(true);
        }
    }

    public void Initialize(BuildProcessPlacer buildProcessPlacer)
    {
        _buildProcessPlacer = buildProcessPlacer;

        _botHub.BotDispatcher.CountChanged += _allCollectorBots.UpdateView;
        _botHub.ResourceCounter.CountChanged += _resourceCounter.UpdateView;
        _botHubBuildButton.Clicked += OnClickBuildBotHubButton;
    }

    public void OnClickBuildBotHubButton(IClickable clickable)
    {
        if (_botHub.Flag.BuildProcess != null && _botHub.Flag.BuildProcess.IsBuilding)
            return;

        if (_botHub.BotDispatcher.AllBotsCount <= 1)
            return;

        if (_buildProcessPlacer != null)
            _buildProcessPlacer.StartPlacement(_botHub.Flag);
    }
}
