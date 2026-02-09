using System;
using UnityEngine;

public class Cell
{
    private Vector3 _worldPosition;
    private Vector2Int _gridPosition;

    public Cell(Vector3 position, Vector2Int gridPosition)
    {
        _worldPosition = position;
        _gridPosition = gridPosition;
    }

    public Vector3 WorldPosition => _worldPosition;
    public Vector2Int GridPosition => _gridPosition;
}