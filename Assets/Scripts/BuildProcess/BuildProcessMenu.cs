using System;
using UnityEngine;

public class BuildProcessMenu : MonoBehaviour, IMenu
{
    [SerializeField] private BuildProcessMenuViwer _menuViwer;
    [SerializeField] private MenuAnimation _menuAnimation;
    [SerializeField] private float _durationAnimation;

    private BuildProcess _buildProcess;

    public event Action<IMenu> Activated;

    public void Show(ClickableObject buildProcess)
    {
        _buildProcess = buildProcess.GetComponent<BuildProcess>();

        Activated?.Invoke(this);
    }

    public void Activate()
    {
        _buildProcess.Timer.Changed += _menuViwer.BuildTime.UpdateView;

        _menuAnimation.OpenMenu(_durationAnimation);
    }

    public void Deactivate()
    {
        _buildProcess.Timer.Changed -= _menuViwer.BuildTime.UpdateView;

        _menuAnimation.CloseMenu(_durationAnimation);
    }
}
