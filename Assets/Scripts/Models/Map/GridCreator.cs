using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridCreator
{
    private const int CellSize = 1;
    private readonly static int s_areaIndex = NavMesh.GetAreaFromName("Walkable");

    private int _areaMask = 1 << s_areaIndex;

    private List<Cell> _allCells = new List<Cell>();
    private Cell[,] _grid;

    private Vector2 _startGrid;
    private Vector2 _endGrid;

    public IReadOnlyList<Cell> AllCells => _allCells;

    public GridCreator(Map map)
    {
        CalculateGridSize(map);
    }

    public void Create()
    {
        float distanceY = 100f;

        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                float positionX = _startGrid.x + i + CellSize / 2;
                float positionZ = _startGrid.y + j + CellSize / 2;

                Vector3 startArea = new Vector3(positionX, distanceY, positionZ);

                if (NavMesh.SamplePosition(startArea, out NavMeshHit hit, distanceY, _areaMask))
                {
                    Vector3 cellWorldPosition = hit.position;
                    Vector2Int cellGridPosition = new Vector2Int(i, j);
                    Cell newCell = new Cell(cellWorldPosition, cellGridPosition);

                    _grid[i, j] = newCell;
                    _allCells.Add(newCell);
                }
            }
        }
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
