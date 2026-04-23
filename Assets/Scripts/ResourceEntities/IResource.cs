using System;
using UnityEngine;

public interface IResource
{
    public Transform Transform { get; }
    public MineralConfig Config { get; }

    public event Action<IResource> Took;
    public event Action<IResource> Unloaded;

    public void Take();
    public void Drop();
    public void ReturnToPool();
}