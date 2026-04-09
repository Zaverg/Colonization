using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour, IGrid
{
    [SerializeField] private int _cellSize;

    private List<List<Cell>> _grid;

    public int CellSizeGrid => _cellSize;
    public int Rows => _grid.Count - 1;

    public void Initialize(List<List<Cell>> grid)
    {
        _grid = grid;
    }

    public Cell GetCell(int row, int column)
    {
        return _grid[row][column];
    }

    public int GetCountColumns(int row)
    {
        return _grid[row].Count - 1;
    }

    public bool IsInGrid(Vector2Int position)
    {
        return (position.x >= 0 && position.y >= 0) && (position.x <= Rows && position.y <= GetCountColumns(position.x));
    }

    public Vector2Int ConvertWorldToGridPosition(Vector3 worldPosition)
    {
        Vector3 startPosition = _grid[0][0].WorldPosition;

        int x = Mathf.RoundToInt((worldPosition - startPosition).x / _cellSize);
        int y = Mathf.RoundToInt((worldPosition - startPosition).z / _cellSize);

        return new Vector2Int(x, y);
    }
}