using System;
using UnityEngine;

public class BaseMenu : MonoBehaviour, IMenu
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BuildProcessPlacer _buildProcessPlacer;
    [SerializeField] private BuildProcessSpawner _buildProcessFactory;

    [Header("Viwers")]
    [SerializeField] private TimerViewer _timerViewer;
    [SerializeField] private BaseMenuViewer _baseMenuViewer;
    [SerializeField] private ResourceCounterViewer _resourceCountViewer;

    [Header("Buttons")]
    [SerializeField] private BaseBuildButton _baseBuildButton;

    private ICollectorBase _collectorBase;

    public event Action<IMenu> OnActiveChanged;

    public ICollectorBase CurrentBase => _collectorBase;

    public void Show(ICollectorBase collectorBase)
    { 
        _collectorBase = collectorBase;

        OnActiveChanged?.Invoke(this);
    }

    public void Activate()
    {
        _collectorBase.ResourceCounter.MineralCountChanged += _resourceCountViewer.UpdateView;
        _collectorBase.Timer.Changed += _timerViewer.UpdateView;
        _baseBuildButton.FlagActivated += _collectorBase.Flag.OnButtonClick;
        //_baseBuildButton.OnBuild += _buildProcessFactory.Spawn;
        _baseBuildButton.OnBuild += _buildProcessPlacer.SpawnBuilder;
        _baseBuildButton.OnPressButton += WaitClick;

        _baseMenuViewer.gameObject.SetActive(true);

        _resourceCountViewer.UpdateView(_collectorBase.ResourceCounter.CollectedResources);
        _timerViewer.UpdateView(_collectorBase.Timer.CurrentSeconds);
    }

    public void Deactivate()
    {
        _collectorBase.ResourceCounter.MineralCountChanged -= _resourceCountViewer.UpdateView;
        _collectorBase.Timer.Changed -= _timerViewer.UpdateView;
        _baseBuildButton.FlagActivated -= _collectorBase.Flag.OnButtonClick;
        //_baseBuildButton.OnBuild -= _buildProcessFactory.Spawn;
        _baseBuildButton.OnBuild -= _buildProcessPlacer.SpawnBuilder;
        _baseBuildButton.OnPressButton -= WaitClick;

        _baseMenuViewer.gameObject.SetActive(false);
    } 

    private void WaitClick()
    {   
        _inputReader.OnClick += OnClick;
    }

    private void OnClick(Transform surface)
    {
        _buildProcessPlacer.TryInstallFlag(surface);
        _inputReader.OnClick -= OnClick;
    }
}
