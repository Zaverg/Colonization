using UnityEngine;
using System;
using System.Collections.Generic;

public interface IGridOccupant
{
    public event Action<IGridOccupant> ReleasedCells;

    public Transform Transform { get; }
    public IReadOnlyList<Vector2Int> OccupyCells { get; }

    public void SetGridArea(List<Vector2Int> area);
    public void SetGridPosition(Vector2Int gridPositon);
}