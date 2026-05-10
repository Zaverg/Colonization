using UnityEngine;

public class BotHubMenuViewer : MonoBehaviour
{
    [SerializeField] private BotHubBuildButton _botHubBuildButton;
    [SerializeField] private CounterViewer _allCollectorBots;
    [SerializeField] private CounterViewer _mineralCounter;

    public BotHubBuildButton BotHubBuildButton => _botHubBuildButton;
    public CounterViewer AllCollectorBots => _allCollectorBots;
    public CounterViewer ResourceCounter => _mineralCounter;
}
