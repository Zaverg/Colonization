using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridCreatorMono : MonoBehaviour, IGrid
{
    private const int CellSize = 1;
    private int s_areaIndex;

    [SerializeField] private GameObject _point;
    [SerializeField] private Map _map;

    private int _areaMask;

    private List<Cell> _allCells = new List<Cell>();
    private Cell[,] _grid;

    private Vector2 _startGrid;
    private Vector2 _endGrid;

    public IReadOnlyList<Cell> AllCells => _allCells;
    public int CellSizeGrid => CellSize;

    public void Awake()
    {
        _map.Initialize();
        s_areaIndex = NavMesh.GetAreaFromName("Walkable");
        _areaMask = 1 << s_areaIndex;
        CalculateGridSize(_map);
        Create();
    }

    public void Create()
    {
        float raycastStartY = 100f;

        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                float halfCell = CellSize / 2f;
                float positionX = _startGrid.x + i + halfCell;
                float positionZ = _startGrid.y + j + halfCell;

                Vector3 startArea = new Vector3(positionX, 0.5f, positionZ);
                Vector3 rayStart = new Vector3(positionX, raycastStartY, positionZ);
                Vector3 rayDirection = Vector3.down;

                if (Physics.Raycast(rayStart, rayDirection, out RaycastHit hit, raycastStartY * 2))
                {
                    if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, halfCell, _areaMask))
                    {
                        Vector3 worldPosition = new Vector3(navHit.position.x, navHit.position.y, navHit.position.z);
                        Vector2Int gridPosition = new Vector2Int(i, j);

                        Instantiate(_point, worldPosition, Quaternion.identity);

                        Cell newCell = new Cell(worldPosition, gridPosition);

                        _grid[i, j] = newCell;
                        _allCells.Add(newCell);
                    }
                }
            }
        }
    }

    public Cell GetCell(int row, int column) 
    { 
        return _grid[row, column];
    }

    private void CalculateGridSize(Map map)
    {
        _startGrid = new Vector2(map.transform.position.x - map.HalfScaleMapX, map.transform.position.z - map.HalfScaleMapZ);
        _endGrid = new Vector2(map.transform.position.x + map.HalfScaleMapX, map.transform.position.z + map.HalfScaleMapZ);

        int rows = Mathf.CeilToInt(_endGrid.x - _startGrid.x);
        int columns = Mathf.CeilToInt(_endGrid.y - _startGrid.y);

        _grid = new Cell[rows, columns];
    }
}

public interface IGrid
{
    public Cell GetCell(int row, int column);
    public IReadOnlyList<Cell> AllCells { get; }
    public int CellSizeGrid { get; }
}