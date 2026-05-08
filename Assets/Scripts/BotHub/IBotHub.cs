using System;
using UnityEngine;

public interface IBotHub
{
    public event Action<BotHub> Disabled;

    public MineralRegistry MineralRegistry { get; }
    public Scanner Scanner { get; }
    public PriceList PriceList { get; }
    public ResourceCounter ResourceCounter { get; }
    public BotDispatcher BotDispatcher { get; }
    public Flag Flag { get; }
    public Transform SpawnBotPlace { get; }
}