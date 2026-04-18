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
    [SerializeField] private List<BuildTypeFactory> _typeFactories;

    public event Action<BuildProcess> Spawned;

    public void Initialize()
    {

    }

    public BuildProcess Spawn(BuildType buildType)
    {
        BuildProcessConfig config = _configs.Where(config => config.BuildType == buildType).FirstOrDefault();
        Debug.Log(config);

        BuildProcess buildProcess = _pool.GetBuildProcess();

        if (buildProcess.BuilderType == BuildType.None)
            buildProcess.Initialize(_grid, _coroutineRunner);

        if (buildProcess.BuilderType != buildType)
        {
            Factory factory = _typeFactories.Where(factoryTypr => factoryTypr.BuildType == buildType).FirstOrDefault().Factory;
            buildProcess.SetConfig(config, factory);
        }

        Spawned?.Invoke(buildProcess);

        return buildProcess;
    }
}

[Serializable]
public class BuildTypeFactory
{
    [SerializeField] private BuildType _buildType;
    [SerializeField] private Factory _factory;

    public BuildType BuildType => _buildType;
    public Factory Factory => _factory;
}