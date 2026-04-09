using UnityEngine;

public class BuildProcessPool : ObjectPool<BuildProcess> 
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public BuildProcess GetBuildProcess()
    {
        BuildProcess buildProcess = GetObject();

        return buildProcess;
    }

    public void PullBuildProcess(BuildProcess buildProcess)
    {
        PutObject(buildProcess);
    }
}