using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellRegisterMono : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private Grid _grid;

    private HashSet<Cell> _freeCells = new HashSet<Cell>();
    private HashSet<Cell> _occupiedCells = new HashSet<Cell>();
    private Dictionary<IResource, List<Cell>> _resourceToCells = new Dictionary<IResource, List<Cell>>();

    public void Awake()
    {
        if (_grid == null)
            return;
        Debug.Log(_grid.Rows);
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

    public void OccupyCell(IResource occupant)
    {
        int index = UnityEngine.Random.Range(0, _freeCells.Count);

        Cell cell = _freeCells.ElementAt(index);

        Debug.Log(cell.GridPosition + " " + cell.WorldPosition + "occupy");
        _freeCells.Remove(cell);
        _occupiedCells.Add(cell);

        // подписка на OnResourceTake

        occupant.Transform.position = cell.WorldPosition;
    }

    public void OccupyCells(List<BuildingShapeUnit> buildingShapeUnits)
    {
        if (buildingShapeUnits == null || buildingShapeUnits.Count == 0)
            return;

        List<Vector2Int> occupyArea = GetOccupyArea(buildingShapeUnits);

        Debug.Log(occupyArea.Count);

        for (int i = 0; i < occupyArea.Count; i++)
        {
            Cell cell = _grid.GetCell(occupyArea[i].x, occupyArea[i].y);

            Debug.Log(cell.GridPosition + " " + cell.WorldPosition + "occupy");
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = cell.WorldPosition;

            _freeCells.Remove(cell);
            _occupiedCells.Add(cell);
        }
    }

    private List<Vector2Int> GetOccupyArea(List<BuildingShapeUnit> buildingShapeUnits)
    {
        Vector2Int rightDownGrid = _grid.ConvertWorldToGridPosition(buildingShapeUnits[0].transform.position);
        Vector2Int leftUpGrid = _grid.ConvertWorldToGridPosition(buildingShapeUnits[1].transform.position);

        Debug.Log(leftUpGrid + " " + rightDownGrid);
        Debug.Log(_grid.GetCell(leftUpGrid.x, leftUpGrid.y).WorldPosition + " " + _grid.GetCell(rightDownGrid.x, rightDownGrid.y).WorldPosition);
        List<Vector2Int> area = new List<Vector2Int>();

        for (int x = leftUpGrid.x; x < rightDownGrid.x + 1; x++)
        {
            for (int y = rightDownGrid.y; y < leftUpGrid.y + 1; y++)
            {
                area.Add(new Vector2Int(x, y));
            }
        }

        return area;
    }

    private void OnResourceTake(IResource collectable)
    {

    }
}
