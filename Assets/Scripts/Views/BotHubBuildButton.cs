using System;
using UnityEngine;
using UnityEngine.UI;

public class BotHubBuildButton : MonoBehaviour, IClickable
{
    public event Action<IClickable> Clicked;

    private bool _active;

    public void OnClick()
    {
        Clicked?.Invoke(this);
    }

    public void SetActive(bool active)
    {
        _active = active;
        GetComponent<Image>().color = _active ? Color.white : Color.gray;
    }
}