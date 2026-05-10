using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridCreator
{
    private readonly static int s_areaIndex = NavMesh.GetAreaFromName("Walkable");

    private int _walkableAreaMask = 1 << s_areaIndex;

    public List<List<Cell>> Create(Map map, int cellSize)
    {
        Vector2 startMap = new Vector2(map.transform.position.x - map.HalfScaleMapX, map.transform.position.z - map.HalfScaleMapZ);
        Vector2 endMap = new Vector2(map.transform.position.x + map.HalfScaleMapX, map.transform.position.z + map.HalfScaleMapZ);

        List<List<Cell>> grid = new List<List<Cell>>();

        (int, int) sizeMap = (Mathf.CeilToInt(endMap.x - startMap.x) / cellSize, 
            Mathf.CeilToInt(endMap.y - startMap.y) / cellSize);

        for (int row = 0; row < sizeMap.Item1; row++)
        {
            List<Cell> columns = new List<Cell>();

            for (int column = 0; column < sizeMap.Item2; column++)
            {
                float halfCell = cellSize / 2f;

                Vector2 surfacePosition = new Vector2(startMap.x + row * cellSize + halfCell, startMap.y + column * cellSize + halfCell);
                Cell cell = TryCreateCell(surfacePosition, grid.Count, columns.Count, halfCell);

                if (cell != null)
                    columns.Add(cell);                
            }

            if (columns.Count > 0)
                grid.Add(columns);
        }

        return grid;
    }

    private Cell TryCreateCell(Vector2 position, int row, int column, float halfCell)
    {
        float raycastStartY = 100f;

        Vector3 rayStart = new Vector3(position.x, raycastStartY, position.y);

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, raycastStartY * 2))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, halfCell, _walkableAreaMask))
            {
                Vector3 worldPosition = new Vector3(navHit.position.x, navHit.position.y, navHit.position.z);
                Vector2Int gridPosition = new Vector2Int(row, column);

                return new Cell(worldPosition, gridPosition);
            }
        }

        return null;
    }
}
