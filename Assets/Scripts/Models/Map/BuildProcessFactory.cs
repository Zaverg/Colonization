using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BuildProcessFactory : MonoBehaviour
{
    [SerializeField] private BuildProcessPool _pool;
    [SerializeField] private List<BuildProcessConfig> _configs;

    private Dictionary<BuildType, BuildProcessConfig> _builderConfig;

    public void Initialize()
    {

    }

    public void Create(BuildType buildType)
    {
        BuildProcessConfig config = _configs.Where(config =>  config.BuildType == buildType).FirstOrDefault();

        BuildProcess buildProcess = _pool.GetBuildProcess();
        buildProcess.SetParams(config);
    }
}