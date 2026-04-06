using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class BuildProcessFactory : MonoBehaviour
{
    [SerializeField] private BuildProcessPool _pool;
    [SerializeField] private List<BuildProcessConfig> _configs;

    private Dictionary<BuildType, BuildProcessConfig> _builderConfig;

    public event Action<BuildProcess> Created;

    public void Initialize()
    {

    }

    public void Create(BuildType buildType)
    {
        BuildProcessConfig config = _configs.Where(config =>  config.BuildType == buildType).FirstOrDefault();

        BuildProcess buildProcess = _pool.GetBuildProcess();
        buildProcess.SetParams(config);

        Created?.Invoke(buildProcess);
    }
}