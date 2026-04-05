public interface IGrid
{
    public Cell GetCell(int row, int column);
    public int CellSizeGrid { get; }
    public int RowsGrid { get; }
    public int ColumnsGrid { get; }
}

