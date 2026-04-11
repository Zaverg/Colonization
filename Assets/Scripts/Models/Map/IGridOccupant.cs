using UnityEngine;
using System;
using System.Collections.Generic;

public interface IGridOccupant
{
    public event Action<IGridOccupant> OnGridOut;

    public Transform Transform { get; }
    public IReadOnlyList<Cell> OccupyCells { get; }

    public void SetGridPosition(List<Cell> cells);
}