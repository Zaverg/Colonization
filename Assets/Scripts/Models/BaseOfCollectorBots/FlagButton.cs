using System;
using UnityEngine;

public class FlagButton : MonoBehaviour, IClickable
{
    public event Action Click;

    public void OnClick()
    {
        Click?.Invoke();
    }
}