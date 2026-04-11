using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class BuildProcessSpawner : MonoBehaviour
{
    [SerializeField] private BuildProcessPool _pool;
    [SerializeField] private List<BuildProcessConfig> _configs;
    [SerializeField] private Grid _grid;
    [SerializeField] private CoroutineRunner _coroutineRunner;

    private Dictionary<BuildType, BuildProcessConfig> _builderConfig;

    public event Action<BuildProcess> Spawned;

    public void Initialize()
    {

    }

    public BuildProcess Spawn(BuildType buildType)
    {
        BuildProcessConfig config = _configs.Where(config =>  config.BuildType == buildType).FirstOrDefault();

        BuildProcess buildProcess = _pool.GetBuildProcess();
        buildProcess.Initialize(config, _grid, _coroutineRunner);

        Spawned?.Invoke(buildProcess);

        return buildProcess;
    }
}