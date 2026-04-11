using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IGridOccupant
{
    public List<Cell> _occupyCells = new List<Cell>();
    public Transform Transform => transform;

    public IReadOnlyList<Cell> OccupyCells => _occupyCells;

    public event Action<IGridOccupant> OnGridOut;

    public void SetGridPosition(List<Cell> cells)
    {
        if (cells == null || cells.Count == 0)
            return;

        _occupyCells = cells;
    }
}