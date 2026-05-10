using UnityEngine;

public class BuildProcessFactory : MonoBehaviour
{
    [SerializeField] private BuildProcess _prefab;
    [SerializeField] private BuildProcessConfig _config;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private BotHubFactory _botHubFactory;

    private IGrid _grid;

    public void Initialize(IGrid grid)
    {
        _grid = grid;
    }

    public BuildProcess Create()
    {
        BuildProcess buildProcess = Instantiate(_prefab);
        buildProcess.Initialize(_grid, _coroutineRunner, _config, _botHubFactory);
        
        TimerViewer timerViewer = buildProcess.GetComponentInChildren<TimerViewer>(true);

        if (timerViewer != null)
        {
            buildProcess.Timer.Changed += timerViewer.UpdateView;
            buildProcess.Released += OnBuildProcessRelease;
        }

        return buildProcess;
    }

    public void OnBuildProcessRelease(BuildProcess buildProcess)
    {
        buildProcess.Released -= OnBuildProcessRelease;

        TimerViewer timerViewer = buildProcess.GetComponentInChildren<TimerViewer>(true);

        if (timerViewer != null)
            buildProcess.Timer.Changed -= timerViewer.UpdateView;

        Destroy(buildProcess);
    }
}
