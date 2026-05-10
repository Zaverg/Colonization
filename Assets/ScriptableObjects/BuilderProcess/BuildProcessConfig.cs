using UnityEngine;

[CreateAssetMenu(fileName = "BuilderProcessConfig", menuName = "Scriptable Objects/BuilderProcessConfig")]
public class BuildProcessConfig : ScriptableObject
{
    [SerializeField] private Transform _prefab;
    [SerializeField] private float _buildDuration;
    private BotHubBuildingAnimation _buildingAnimation;

    public Transform Prefab => _prefab;
    public float BuildTime => _buildDuration;

    public BotHubBuildingAnimation BuildingAnimation 
    { 
        get
        {
            if (_buildingAnimation == null)
               _buildingAnimation = new BotHubBuildingAnimation();

            return _buildingAnimation;

        } 
    }
}