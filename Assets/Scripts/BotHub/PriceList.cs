using UnityEngine;

public class PriceList : MonoBehaviour
{
    [SerializeField] private int _countResourceToCreateBot;
    [SerializeField] private int _countResourceToBuildBotHub;

    public int CountResourceToCreateBot => _countResourceToCreateBot;
    public int CountResourceToBuildBotHub => _countResourceToBuildBotHub;
}