using System.Collections.Generic;

public class Grid : IGrid
{
    private int _cellSize;

    private List<Cell> _allCells = new List<Cell>();
    private Cell[,] _position;

    public IReadOnlyList<Cell> AllCells => _allCells;

    public int CellSizeGrid => _cellSize;

    public Grid(List<Cell> allCells, Cell[,] positions, int cellSize)
    {
        _allCells = allCells;
        _position = positions;
        _cellSize = cellSize;
    }


    public Cell GetCell(int row, int column)
    {
        return _position[row, column];
    }
}