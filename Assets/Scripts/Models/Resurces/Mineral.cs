using System;
using UnityEngine;

public class Mineral : Resource, IReleasable<Mineral>
{
    [SerializeField] private MineralConfig _mineralConfig;

    public event Action<Mineral> Released;
    public override event Action<IResource> Took;
    public override event Action<IResource> Unlodered;
    public override event Action<IGridOccupant> OnGridOut;

    public override void Take()
    {
        Took?.Invoke(this);
        OnGridOut?.Invoke(this);
    }

    public override void Drop()
    {
        Unlodered?.Invoke(this);
    }

    public override void ReturnToPool()
    {
        Released?.Invoke(this);
    }
}
