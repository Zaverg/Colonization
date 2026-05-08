using System;
using UnityEngine;

public class BotHubMenuViewer : MonoBehaviour
{
    [SerializeField] private TimerViewer _timerViewer;
    [SerializeField] private CounterViewer _resources;
    [SerializeField] private BotHubBuildButton _botHubBuildButton;
    [SerializeField] private CounterViewer _allCollectorBots;

    public TimerViewer TimerViewer => _timerViewer;
    public CounterViewer Resource => _resources;
    public BotHubBuildButton BotHubBuildButton => _botHubBuildButton;
    public CounterViewer AllCollectorBots => _allCollectorBots;
}
