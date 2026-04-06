using UnityEngine;

[CreateAssetMenu(fileName = "BuilderProcessConfig", menuName = "Scriptable Objects/BuilderProcessConfig")]
public class BuildProcessConfig : ScriptableObject
{
    [SerializeField] private BuildType _buildType;
    [SerializeField] private Transform _prefab;

    public BuildType BuildType => _buildType;
    public Transform Prefab => _prefab;
}