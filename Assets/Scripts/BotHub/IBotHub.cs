using System;
using UnityEngine;

public interface IBotHub
{
    public int CountResourceToCreateBot { get; }
    public int CountResourceToBuildBase { get; }

    public event Action<IBotHub> Click;
    public event Action<IBotHub> Disabled; 

    public Timer Timer { get; }
    public ResourceCounter ResourceCounter { get; }

    public BotDispatcher BotDispatcher { get; }
    public Flag Flag { get; }
    public MineralRegistry MineralRegistry { get; }
    public CollectorBotSpawner CollectorBotSpawner { get; }
    public Transform SpawnBotPlace { get; }
    public CollectorBaseTask MainTask { get; }
}