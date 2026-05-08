using System;
using UnityEngine;
using UnityEngine.UI;

public class BotHubBuildButton : MonoBehaviour, IClickable
{
    public event Action<BuildType> OnBuild;
    public event Action OnPressButton;
    public event Action<ClickableObject> Click;

    private bool _active;

    public void OnClick()
    {
        if (_active == false)
            return;

        OnBuild?.Invoke(BuildType.CollectorBase);
        OnPressButton?.Invoke();
    }

    public void SetActive(bool active)
    {
        _active = active;
        GetComponent<Image>().color = _active ? Color.white : Color.gray;
    }
}