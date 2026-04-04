using System.Collections.Generic;

public interface IGrid
{
    public Cell GetCell(int row, int column);
    public IReadOnlyList<Cell> AllCells { get; }
    public int CellSizeGrid { get; }
}

