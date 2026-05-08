using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class BuildProcessFactory : MonoBehaviour
{
    [SerializeField] private BuildProcessPool _pool;
    [SerializeField] private List<BuildProcessConfig> _configs;
    [SerializeField] private Grid _grid;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private List<BuildTypeFactory> _typeFactories;
    [SerializeField] private BuildProcessMenu _buildProcessMenu;

    public event Action<BuildProcess> Spawned;

    public BuildProcess Create(BuildType buildType)
    {
        BuildProcessConfig config = _configs.Where(config => config.BuildType == buildType).FirstOrDefault();

        BuildProcess buildProcess = _pool.PullBuildProcess();

        if (buildProcess.BuilderType == BuildType.None)
        {
            buildProcess.Initialize(_grid, _coroutineRunner);

        }

        BuildFactory factory = _typeFactories.Where(factoryTypr => factoryTypr.BuildType == buildType).FirstOrDefault().Factory;
        buildProcess.SetConfig(config, factory);
        
        buildProcess.GetComponent<ClickableObject>().Click += _buildProcessMenu.Show;
        buildProcess.Released += OnBuildProcessReleased;

        Spawned?.Invoke(buildProcess);

        return buildProcess;
    }

    public void OnBuildProcessReleased(BuildProcess buildProcess)
    {
        buildProcess.GetComponent<ClickableObject>().Click -= _buildProcessMenu.Show;
    }
}
