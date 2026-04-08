using UnityEngine;

public class BuildProcessPool : ObjectPool<BuildProcess> 
{
    [SerializeField] private Grid _grid;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BaseMenu _baseMenu;

    public override void Initialize()
    {
        base.Initialize();
    }

    public BuildProcess GetBuildProcess()
    {
        BuildProcess buildProcess = GetObject();
        buildProcess.Initialize(_grid);

        return buildProcess;
    }

    public void PullBuildProcess(BuildProcess buildProcess)
    {
        PutObject(buildProcess);
    }
}