using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class CellRegister : MonoBehaviour
{
    [SerializeField] private Map _map;

    private HashSet<Cell> _freeCells = new HashSet<Cell>();
    private HashSet<Cell> _occupiedCells = new HashSet<Cell>();

    private Dictionary<IResource, Cell> _resourceToCells = new Dictionary<IResource, Cell>();

    public void Initialize(IReadOnlyList<Cell> cells)
    {
        _freeCells = new HashSet<Cell>(cells);

        gameObject.SetActive(true);
    }

    public void OccupyCell(IResource mineral)
    {
        mineral.Taked += OnResourceTake;
     
        int index = Random.Range(0, _freeCells.Count);

        Cell cell = _freeCells.ElementAt(index);
        _resourceToCells[mineral] = cell;

        mineral.Transform.position = cell.WorldPosition;

        _freeCells.Remove(cell);
        _occupiedCells.Add(cell);
    }

    public void OccupyArea(IGridOccupant occupant)
    {
     
    }
   
    private void OnResourceTake(IResource collectable)
    {
        collectable.Taked -= OnResourceTake;

        Cell cell = _resourceToCells[collectable];

        _occupiedCells.Remove(cell);
        _freeCells.Add(cell);

        _resourceToCells.Remove(collectable);
    }
}

public interface IGridOccupant
{
    public Vector2Int GridPosition { get;  }
    public Vector3 WorldPosition { get; }
}

public class GridOccupant : MonoBehaviour, IGridOccupant
{
    public Vector2Int GridPosition => throw new System.NotImplementedException();

    public Vector3 WorldPosition => throw new System.NotImplementedException();
}

public class CellRegisterMono : MonoBehaviour
{
    private IGrid _grid;

    private HashSet<Cell> _freeCells = new HashSet<Cell>();
    private HashSet<Cell> _occupiedCells = new HashSet<Cell>();

    private Dictionary<IResource, Cell> _resourceToCells = new Dictionary<IResource, Cell>();

    public void Initialize(IGrid grid)
    {
        _freeCells = new HashSet<Cell>(grid.AllCells);

        gameObject.SetActive(true);
    }

    public void OccupyCells(List<BuildingShapeUnit> buildingShapeUnits)
    {
        if (buildingShapeUnits == null || buildingShapeUnits.Count == 0)
            return;

        if (buildingShapeUnits.Count == 1)
        {
            Vector2Int cellGridPosition = ConvertWorldToGridPosition(buildingShapeUnits[0].transform.position);
            Cell cell = _grid.GetCell(cellGridPosition.x, cellGridPosition.y);

            _freeCells.Remove(cell);
            _occupiedCells.Add(cell);

            // подписка на событие для возврата в freeCells
        }

        List<Vector2Int> occupyArea = GetOccupyArea(buildingShapeUnits);

        for (int i = 0; i < occupyArea.Count; i++)
        {
            Cell cell = _grid.GetCell(occupyArea[i].x, occupyArea[i].y);

            _freeCells.Remove(cell);
            _occupiedCells.Add(cell);
        }
    }

    private Vector2Int ConvertWorldToGridPosition(Vector3 worldPosition)
    {
        Vector3 startPosition = _grid.GetCell(0, 0).WorldPosition;

        int x = Mathf.FloorToInt((worldPosition - startPosition).x / _grid.CellSizeGrid);
        int y = Mathf.FloorToInt((worldPosition - startPosition).z / _grid.CellSizeGrid);
        
        return new Vector2Int(x, y);
    }

    private List<Vector2Int> GetOccupyArea(List<BuildingShapeUnit> buildingShapeUnits)
    {
        Vector3 rightDown = buildingShapeUnits[0].transform.position;
        Vector3 leftUp = buildingShapeUnits[1].transform.position;

        foreach (BuildingShapeUnit shape in buildingShapeUnits)
        {
            if (shape.transform.position.z > rightDown.z && shape.transform.position.x < rightDown.z)
                rightDown = shape.transform.position;
            else if (shape.transform.position.z < leftUp.z && shape.transform.position.x > leftUp.x)
                leftUp = shape.transform.position;
        }

        Vector2Int rightDownGrid = ConvertWorldToGridPosition(rightDown);
        Vector2Int leftUpGrid = ConvertWorldToGridPosition(leftUp);

        List<Vector2Int> area = new List<Vector2Int>();

        for (int x = rightDownGrid.x; x < leftUpGrid.x; x++)
        {
            for (int y = rightDownGrid.y; y < leftUpGrid.y; y++)
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

public class Builder : MonoBehaviour
{

}