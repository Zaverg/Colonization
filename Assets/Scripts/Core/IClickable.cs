using System;

public interface IClickable
{
    public event Action<IClickable> Clicked;

    public void OnClick();
}