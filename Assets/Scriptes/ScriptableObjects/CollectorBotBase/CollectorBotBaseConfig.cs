using UnityEngine;

[CreateAssetMenu(fileName = "CollecotrBotBaseConfig", menuName = "Scriptable Objects/CollecotrBotBaseConfig")]
public class CollectorBotBaseConfig : ScriptableObject
{
    [SerializeField] private float _scanInterval;
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _scanLayer;

    public float ScanInterval => _scanInterval;
    public float ScanRadius => _scanRadius;
    public LayerMask ScanLayer => _scanLayer;
}
