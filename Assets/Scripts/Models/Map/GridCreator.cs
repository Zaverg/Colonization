using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridCreator
{
    private const int CellSize = 1;
    private readonly static int s_areaIndex = NavMesh.GetAreaFromName("Walkable");

    private int _areaMask = 1 << s_areaIndex;

    public Grid Create(Map map)
    {
        Vector2 startGrid = new Vector2(map.transform.position.x - map.HalfScaleMapX, map.transform.position.z - map.HalfScaleMapZ);
        Vector2 endGrid = new Vector2(map.transform.position.x + map.HalfScaleMapX, map.transform.position.z + map.HalfScaleMapZ);

        Vector2Int sizeGrid = CalculateGridSize(startGrid, endGrid);

        float raycastStartY = 100f;

        int row = 0;
        int column = 0;

        List<Cell> allCells = new List<Cell>();
        Cell[,] positions = new Cell[sizeGrid.x, sizeGrid.y];

        for (int i = 0; i < positions.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < positions.GetLength(1) - 1; j++)
            {
                float halfCell = CellSize / 2f;
                float positionX = startGrid.x + i + halfCell;
                float positionZ = startGrid.y + j + halfCell;

                Vector3 rayStart = new Vector3(positionX, raycastStartY, positionZ);
                Vector3 rayDirection = Vector3.down;

                if (Physics.Raycast(rayStart, rayDirection, out RaycastHit hit, raycastStartY * 2))
                {
                    if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, halfCell, _areaMask))
                    {
                        Vector3 worldPosition = new Vector3(navHit.position.x, navHit.position.y, navHit.position.z);
                        Vector2Int gridPosition = new Vector2Int(row, column);

                        Cell newCell = new Cell(worldPosition, gridPosition);

                        positions[row, column] = newCell;
                        allCells.Add(newCell);

                        column++;
                    }
                }
            }

            if (column > 0)
            {
                column = 0;
                row++;
            }
        }

        return new Grid(allCells, positions, CellSize);
    }

    private Vector2Int CalculateGridSize(Vector2 start, Vector2 end)
    {
        int rows = Mathf.CeilToInt(end.x - start.x);
        int columns = Mathf.CeilToInt(end.y - start.y);

        return new Vector2Int(rows, columns);
    }
}
