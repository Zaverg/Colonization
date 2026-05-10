using System;

public interface IMenu
{
    public event Action<IMenu> Activated;

    public void Show(IClickable clickable);
    public void Activate();
    public void Deactivate();
}