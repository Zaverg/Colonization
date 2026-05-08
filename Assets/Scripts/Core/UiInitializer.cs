using UnityEngine;

public class UiInitializer: MonoBehaviour
{
    [SerializeField] private BotHubMenu _botHubMenu;
    [SerializeField] private BuildProcessMenu _buildProcessMenu;
    [SerializeField] private MenuActivator _menuActivator;
    [SerializeField] private InputReader _inputReader;

    public void Initialize()
    {
        _inputReader.gameObject.SetActive(false);

        _menuActivator = new MenuActivator();
        _inputReader.Initialize();
    }

    public void Subscribe()
    {
        _botHubMenu.Activated += _menuActivator.SwitchActiveMenu;
        _buildProcessMenu.Activated += _menuActivator.SwitchActiveMenu;
        _inputReader.OnClick += _menuActivator.OnClosedMenu;
    }

    public void Unsubscribe()
    {
        _botHubMenu.Activated -= _menuActivator.SwitchActiveMenu;
        _buildProcessMenu.Activated -= _menuActivator.SwitchActiveMenu;
        _inputReader.OnClick -= _menuActivator.OnClosedMenu;
    }

    public void OnBaseCreated(BotHub botHub)
    {
        botHub.Disabled += OnBaseDisabled;

        botHub.GetComponent<ClickableObject>().Click += _botHubMenu.Show;
    }

    public void OnBaseDisabled(BotHub botHub)
    {
        botHub.Disabled -= OnBaseDisabled;
        botHub.GetComponent<ClickableObject>().Click -= _botHubMenu.Show;
    }
}