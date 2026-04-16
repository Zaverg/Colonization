using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IGridOccupant
{
    public List<Vector2Int> _occupyCells = new List<Vector2Int>();
    public Transform Transform => transform;

    public IReadOnlyList<Vector2Int> OccupyCells => _occupyCells;

    public event Action<IGridOccupant> OnFreeCells;

    public void SetGridArea(List<Vector2Int> area)
    {
        if (area == null || area.Count == 0)
            return;

        _occupyCells = area;
    }

    public void SetGridPosition(Vector2Int gridPosition)
    {
        _occupyCells.Clear();   
        _occupyCells.Add(gridPosition);
    }
}