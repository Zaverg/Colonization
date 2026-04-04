using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuilderProcessConfig", menuName = "Scriptable Objects/BuilderProcessConfig")]
public class BuildProcessConfig : ScriptableObject
{
    [SerializeField] private BuildType _buildType;
    [SerializeField] private Transform _prefab;
    [SerializeField] private List<Vector3> _shapeLocalPosition;

    public BuildType BuildType => _buildType;
    public IReadOnlyList<Vector3> ShapeLocalPosition => _shapeLocalPosition;
    public Vector3 Rotation => _prefab.eulerAngles;
    public Vector3 Scale => _prefab.localScale;
}