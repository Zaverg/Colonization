using UnityEngine;

public class Grid : MonoBehaviour, IGrid
{
    [SerializeField] private int _cellSize;

    private Cell[,] _grid;

    private int _rows;
    private int _columns;

    public int CellSizeGrid => _cellSize;

    public int RowsGrid => _rows;
    public int ColumnsGrid => _columns;

    public void Inicialize(Cell[,] grid)
    {
        _grid = grid;

        _rows = _grid.GetLength(0);
        _columns = _grid.GetLength(1);
    }

    public Cell GetCell(int row, int column)
    {
        return _grid[row, column];
    }

    public Vector2Int ConvertWorldToGridPosition(Vector3 worldPosition)
    {
        Vector3 startPosition = _grid[0, 0].WorldPosition;

        int x = Mathf.RoundToInt((worldPosition - startPosition).x / _cellSize);
        int y = Mathf.RoundToInt((worldPosition - startPosition).z / _cellSize);

        return new Vector2Int(x, y);
    }
}