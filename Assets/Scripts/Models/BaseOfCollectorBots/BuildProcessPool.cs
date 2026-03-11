public class BuildProcessPool : ObjectPool<BuildProcess> 
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public BuildProcess GetBuildProcess()
    {
        return GetObject();
    }
}