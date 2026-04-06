using UnityEngine;

public class BuildProcessPool : ObjectPool<BuildProcess> 
{
    [SerializeField] private Grid _grid;

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
}