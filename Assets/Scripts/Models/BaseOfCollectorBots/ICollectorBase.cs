using System;
using UnityEngine;

public interface ICollectorBase
{
    public int CountResourceToCreateBot { get; }
    public int CountResourceToBuildBase { get; }

    public event Action<CollectorBotBase> Click;

    public Timer Timer { get; }
    public ResourceCounter ResourceCounter { get; }

    public CollectorBotDispatcher BotDispatcher { get; }
    public Flag Flag { get; }
    public MineralRegistry MineralRegistry { get; }
    public IFactory FactoryBot { get; }
    public Transform SpawnBotPlace { get; }

    public void PlaceFlag();
    public void SetFlag(Flag flag);
    public CollectorBot TryCreateCollectorBot();
}