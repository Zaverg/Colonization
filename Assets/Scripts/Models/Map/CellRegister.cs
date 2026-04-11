using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class CellRegister : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private Grid _grid;

    private HashSet<Cell> _freeCells = new HashSet<Cell>();
    private HashSet<Cell> _occupiedCells = new HashSet<Cell>();
    private Dictionary<IGridOccupant, List<Cell>> _objectToCells = new Dictionary<IGridOccupant, List<Cell>>();

    public void Initialize()
    {
        if (_grid == null)
            return;

        for (int row = 0; row < _grid.Rows; row++)
        {
            int columns = _grid.GetCountColumns(row);

            for (int column = 0; column < columns; column++)
            {
                _freeCells.Add(_grid.GetCell(row, column));
            }
        }

        gameObject.SetActive(true);
    }

    public void OccupyCell(IGridOccupant occupant)
    {
        int index = UnityEngine.Random.Range(0, _freeCells.Count);

        Cell cell = _freeCells.ElementAt(index);

        Debug.Log(cell.GridPosition + " " + cell.WorldPosition + "occupy");
        _freeCells.Remove(cell);
        _occupiedCells.Add(cell);

        _objectToCells[occupant] = new List<Cell>();
        _objectToCells[occupant].Add(cell);

        occupant.OnGridOut += OnFreeCells;

        occupant.Transform.position = cell.WorldPosition;
    }

    public void OccupyCells(List<Vector2Int> occupyArea, IGridOccupant gridOccupant)
    {
        gridOccupant.OnGridOut += OnFreeCells;
        List<Cell> cells = new List<Cell>();

        for (int i = 0; i < occupyArea.Count; i++)
        {
            Cell cell = _grid.GetCell(occupyArea[i].x, occupyArea[i].y);

            Debug.Log(cell.GridPosition + " " + cell.WorldPosition + "occupy");

            _freeCells.Remove(cell);
            _occupiedCells.Add(cell);

            cells.Add(cell);
        }

        _objectToCells[gridOccupant] = cells;
    }

    public List<Vector2Int> TryGetOccupyArea(List<BuildingShapeUnit> buildingShapeUnits)
    {
        List<Vector2Int> area = new List<Vector2Int>();

        if (buildingShapeUnits == null || buildingShapeUnits.Count == 0)
            return area;

        Vector2Int rightDownGrid = _grid.ConvertWorldToGridPosition(buildingShapeUnits[0].transform.position);
        Vector2Int leftUpGrid = _grid.ConvertWorldToGridPosition(buildingShapeUnits[1].transform.position);

        Debug.Log(rightDownGrid);
        Debug.Log(leftUpGrid);

        if (_grid.IsInGrid(rightDownGrid) == false || _grid.IsInGrid(leftUpGrid) == false)
            return area;

        for (int x = leftUpGrid.x; x < rightDownGrid.x + 1; x++)
        {

            for (int y = rightDownGrid.y; y < leftUpGrid.y + 1; y++)
            {
                Debug.Log(_freeCells.Contains(_grid.GetCell(x, y)));
                if (_freeCells.Contains(_grid.GetCell(x, y)) == false)
                {
                    area.Clear();

                    return area;
                }

                area.Add(new Vector2Int(x, y));
            }
        }

        return area;
    }

    private void OnFreeCells(IGridOccupant occupant)
    {
        occupant.OnGridOut -= OnFreeCells;
        Debug.Log("Free");

        if (_objectToCells.ContainsKey(occupant))
        {
            List<Cell> cells = _objectToCells[occupant];

            _freeCells.AddRange(cells);
         
            foreach (Cell cell in cells)
            {
                Debug.Log(cell.GridPosition + " " + cell.WorldPosition + "free");
                _occupiedCells.Remove(cell);
            }

            _objectToCells.Remove(occupant);
        }
    }
}