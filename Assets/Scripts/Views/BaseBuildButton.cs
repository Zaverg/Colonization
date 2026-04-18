using System;
using UnityEngine;

public class BaseBuildButton : MonoBehaviour, IClickable
{
    public event Action<CollectorBotTaskName> FlagActivated;
    public event Action<BuildType> OnBuild;
    public event Action OnPressButton;

    public void OnClick()
    {
        FlagActivated?.Invoke(CollectorBotTaskName.BaseBuild);
        OnBuild?.Invoke(BuildType.CollectorBase);
        OnPressButton?.Invoke();
    }
}