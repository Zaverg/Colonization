using UnityEngine;

[CreateAssetMenu(fileName = "MineralConfig", menuName = "Scriptable Objects/MineralConfig")]
public class MineralConfig : ScriptableObject
{
    [SerializeField] private MineralType _type;

    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;

    [SerializeField] private float _miningDuration;
    [SerializeField] private float _rarity;

    public MineralType Type => _type;

    public Mesh Mesh => _mesh;
    public Material Material => _material;

    public float MiningDuration => _miningDuration;
    public float Rarity => _rarity;
}
