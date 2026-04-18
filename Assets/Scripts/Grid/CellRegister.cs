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

    public void OccupyRandomCell(IGridOccupant occupant)
    {
        int index = UnityEngine.Random.Range(0, _freeCells.Count);

        Cell cell = _freeCells.ElementAt(index);

        _freeCells.Remove(cell);
        _occupiedCells.Add(cell);
       
        occupant.SetGridPosition(cell.GridPosition);
        occupant.OnFreeCells += OnFreeCells;

        occupant.Transform.position = cell.WorldPosition;
    }

    public void ReserveArea(List<Vector2Int> occupyArea)
    {
        foreach (Vector2Int position in occupyArea)
        {
            Cell cell = _grid.GetCell(position.x, position.y);

            _freeCells.Remove(cell);

            Debug.Log("Reserve cell - " + cell.GridPosition);
        }
    }

    public void OccupyArea(IGridOccupant occupant)
    {
        occupant.OnFreeCells += OnFreeCells;

        foreach (Vector2Int position in occupant.OccupyCells)
        {
            Cell cell = _grid.GetCell(position.x, position.y);

            if (_freeCells.Contains(cell))
                _freeCells.Remove(cell);

            _occupiedCells.Add(cell);

            Debug.Log("Occupy cell - " + cell.GridPosition);
        }
    }

    public List<Vector2Int> TryGetOccupyArea(List<BuildingShapeUnit> buildingShapeUnits)
    {
        List<Vector2Int> area = new List<Vector2Int>();

        if (buildingShapeUnits == null || buildingShapeUnits.Count == 0)
            return area;

        Vector2Int rightDownGrid = _grid.ConvertWorldToGridPosition(buildingShapeUnits[0].transform.position);
        Vector2Int leftUpGrid = _grid.ConvertWorldToGridPosition(buildingShapeUnits[1].transform.position);

        if (_grid.IsInGrid(rightDownGrid) == false || _grid.IsInGrid(leftUpGrid) == false)
            return area;

        for (int x = leftUpGrid.x; x < rightDownGrid.x + 1; x++)
        {
            for (int y = rightDownGrid.y; y < leftUpGrid.y + 1; y++)
            {
                Cell cell = _grid.GetCell(x, y);

                if (_freeCells.Contains(cell) == false)
                {
                    area.Clear();

                    return area;
                }

                area.Add(new Vector2Int(x, y));
            }
        }

        return area;
    }

    public void FreeCells(List<Vector2Int> area)
    {
        foreach (Vector2Int position in area)
        {
            Cell cell = _grid.GetCell(position.x, position.y);

            if (cell != null && _freeCells.Contains(cell) == false)
            {
                _occupiedCells.Remove(cell);
                _freeCells.Add(cell);

                Debug.Log("Free cell - " + cell.GridPosition);
            }
        }
    }

    private void OnFreeCells(IGridOccupant occupant)
    {
        occupant.OnFreeCells -= OnFreeCells;
        FreeCells(occupant.OccupyCells.ToList());
    }
}