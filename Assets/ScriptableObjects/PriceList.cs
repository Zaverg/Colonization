using UnityEngine;

[CreateAssetMenu(fileName = "PriceList", menuName = "Scriptable Objects/PriceList")]
public class PriceList : ScriptableObject
{
    [SerializeField] private int _countResourceToCreateBot;
    [SerializeField] private int _countResourceToBuildBotHub;

    public int CountResourceToCreateBot => _countResourceToCreateBot;
    public int CountResourceToBuildBotHub => _countResourceToBuildBotHub;
}
