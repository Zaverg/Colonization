using System;

public interface IMenu
{
    public event Action<IMenu> Activated;

    public void Show(ClickableObject clickable);
    public void Activate();
    public void Deactivate();
}