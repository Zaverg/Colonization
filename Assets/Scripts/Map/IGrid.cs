using UnityEngine;

public interface IGrid
{
    public int CellSizeGrid { get; }
    public int Rows { get; }

    public Cell GetCell(int row, int column);

    public int GetCountColumns(int row);

    public bool IsInGrid(Vector2Int position);

    public Vector2Int ConvertWorldToGridPosition(Vector3 worldPosition);
}

