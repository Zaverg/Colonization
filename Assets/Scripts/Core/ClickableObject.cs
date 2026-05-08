using UnityEngine;
using System;

public class ClickableObject : MonoBehaviour, IClickable
{
    public event Action<ClickableObject> Click;

    public void OnClick()
    {
        Click?.Invoke(this);
    }
}