using UnityEngine;

public class BuildProcessPool : ObjectPool<BuildProcess> 
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public BuildProcess PullBuildProcess()
    {
        BuildProcess buildProcess = PullObject();

        return buildProcess;
    }

    public void PutBuildProcess(BuildProcess buildProcess)
    {
        PutObject(buildProcess);
    }
}