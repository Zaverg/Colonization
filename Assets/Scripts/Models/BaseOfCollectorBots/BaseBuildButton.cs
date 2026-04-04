using System;
using UnityEngine;

public class BaseBuildButton : MonoBehaviour, IClickable
{
    public event Action<CollectorBotTaskName> FlagActivated;
    public event Action<BuildType> OnBuild;

    public void OnClick()
    {
        FlagActivated?.Invoke(CollectorBotTaskName.BaseBuild);
        OnBuild?.Invoke(BuildType.CollectorBase);
    }
}