using System;
using UnityEngine;

public class BaseBuildButton : MonoBehaviour, IClickable
{
    public event Action<CollectorBotTaskName> FlagActivated;

    public void OnClick()
    {
        FlagActivated?.Invoke(CollectorBotTaskName.BaseBuild);
    }
}