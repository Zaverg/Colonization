using System;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : Resource, IReleasable<Mineral>
{
    [SerializeField] private MineralConfig _mineralConfig;

    public event Action<Mineral> Released;
    public override event Action<IResource> Took;
    public override event Action<IResource> Unloaded;
    public override event Action<IGridOccupant> ReleasedCells;

    public override void Take()
    {
        Took?.Invoke(this);
        ReleasedCells?.Invoke(this);
    }

    public override void Drop()
    {
        Unloaded?.Invoke(this);
    }

    public override void ReturnToPool()
    {
        _occupyArea.Clear();
        Released?.Invoke(this);
    }

    public override void SetGridArea(List<Vector2Int> area)
    {
        if (area == null || area.Count == 0)
            return;

        _occupyArea = area;
    }

    public override void SetGridPosition(Vector2Int gridPositon)
    {
        _occupyArea.Add(gridPositon);
    }
}
