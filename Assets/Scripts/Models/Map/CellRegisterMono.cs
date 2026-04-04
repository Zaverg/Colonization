using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class CellRegisterMono : MonoBehaviour
{
    [SerializeField] private List<BuildingShapeUnit> _shapes = new List<BuildingShapeUnit>();
    [SerializeField] private GridCreatorMono _grid;

    private HashSet<Cell> _freeCells = new HashSet<Cell>();
    private HashSet<Cell> _occupiedCells = new HashSet<Cell>();

    private Dictionary<IResource, Cell> _resourceToCells = new Dictionary<IResource, Cell>();

    private void Awake()
    {
        _freeCells = new HashSet<Cell>(_grid.AllCells);

        OccupyCells(_shapes);
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

    private Vector2Int ConvertWorldToGridPosition(Vector3 worldPosition)
    {
        Vector3 startPosition = _grid.GetCell(0, 0).WorldPosition;

        int x = Mathf.RoundToInt((worldPosition - startPosition).x / _grid.CellSizeGrid);
        int y = Mathf.RoundToInt((worldPosition - startPosition).z / _grid.CellSizeGrid);
        
        return new Vector2Int(x, y);
    }

    private List<Vector2Int> GetOccupyArea(List<BuildingShapeUnit> buildingShapeUnits)
    {
        Vector2Int rightDownGrid = ConvertWorldToGridPosition(buildingShapeUnits[0].transform.position);
        Vector2Int leftUpGrid = ConvertWorldToGridPosition(buildingShapeUnits[1].transform.position);

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
