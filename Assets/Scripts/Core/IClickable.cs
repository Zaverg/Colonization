using System;
using UnityEngine;

public interface IClickable
{
    public event Action<ClickableObject> Click;

    public void OnClick();
}