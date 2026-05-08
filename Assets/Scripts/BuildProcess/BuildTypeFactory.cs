using UnityEngine;
using System;

[Serializable]
public class BuildTypeFactory
{
    [SerializeField] private BuildType _buildType;
    [SerializeField] private BuildFactory _factory;

    public BuildType BuildType => _buildType;
    public BuildFactory Factory => _factory;
}