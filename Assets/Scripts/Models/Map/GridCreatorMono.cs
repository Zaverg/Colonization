using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridCreatorMono : MonoBehaviour
{
    private const int CellSize = 1;
    private int s_areaIndex;

    [SerializeField] private GameObject _point;
    [SerializeField] private Map _map;
    [SerializeField] private Grid _grid;

    private int _areaMask;

    private List<List<Cell>> _allCells = new List<List<Cell>>();

    private Vector2 _startGrid;
    private Vector2 _endGrid;
    public int CellSizeGrid => CellSize;

    private void Awake()
    {
        _map.Initialize();

        s_areaIndex = NavMesh.GetAreaFromName("Walkable");
        _areaMask = 1 << s_areaIndex;
        _grid.Initialize(Create(_map, CellSize));
    }

    public List<List<Cell>> Create(Map map, int cellSize)
    {
        Vector2 startMap = new Vector2(map.transform.position.x - map.HalfScaleMapX, map.transform.position.z - map.HalfScaleMapZ);
        Vector2 endMap = new Vector2(map.transform.position.x + map.HalfScaleMapX, map.transform.position.z + map.HalfScaleMapZ);

        List<List<Cell>> grid = new List<List<Cell>>();

        (int, int) sizeMap = (Mathf.CeilToInt(endMap.x - startMap.x) / cellSize, Mathf.CeilToInt(endMap.y - startMap.y) / cellSize);

        Debug.Log(sizeMap);

        for (int i = 0; i < sizeMap.Item1; i += cellSize)
        {
            for (int j = 0; j < sizeMap.Item2; j += cellSize)
            {
                List<Cell> columns = new List<Cell>();

                Vector2 surfacePosition = new Vector2(startMap.x + i + cellSize / 2, startMap.y + j + cellSize / 2);
                Cell cell = TryCreateCell(surfacePosition, grid.Count, columns.Count);

                if (cell != null)
                    columns.Add(cell);
            }
        }

        return grid;
    }

    private Cell TryCreateCell(Vector2 position, int row, int column)
    {
        float raycastStartY = 100f;
        float distance = CellSize / 2f;

        Vector3 rayStart = new Vector3(position.x, raycastStartY, position.y);

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, raycastStartY * 2))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, distance, _areaMask))
            {
                Vector3 worldPosition = new Vector3(navHit.position.x, navHit.position.y, navHit.position.z);
                Vector2Int gridPosition = new Vector2Int(row, column);

                return new Cell(worldPosition, gridPosition);
            }
        }

        return null;
    }
}