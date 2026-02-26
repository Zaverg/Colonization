using System;
using UnityEngine;

public interface IResource
{
    public Transform Transform { get; }
    public MineralConfig Config { get; }

    public event Action<IResource> Taked;
    public event Action<IResource> Unlodered;

    public void Take();
    public void Drop();
    public void ReturnToPool();
}