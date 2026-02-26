using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridCreator
{
    private readonly static int s_areaIndex = NavMesh.GetAreaFromName("Walkable");
    private List<Cell> _allCells = new List<Cell>();

    public IReadOnlyList<Cell> AllCells => _allCells;

    public Cell[,] Create(Map map)
    {
        (float, float) start = ((map.transform.position.x - map.HalfScaleMapX), 
            (map.transform.position.z - map.HalfScaleMapZ));

        (float, float) end = ((map.transform.position.x + map.HalfScaleMapX), 
            (map.transform.position.z + map.HalfScaleMapZ));

        int cellSizeX = 1;
        int cellSizeZ = 1;

        int rows = Mathf.CeilToInt((end.Item1 -  start.Item1) /  cellSizeX);
        int columns = Mathf.CeilToInt((end.Item2 - start.Item2) / cellSizeZ);

        int areaMask = 1 << s_areaIndex;

        Cell[,] grid = new Cell[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                float centerCellX = cellSizeX / 2;
                float centerCellZ = cellSizeZ / 2;

                float startCellX = i * cellSizeX;
                float startCellZ = j * cellSizeZ;

                float positionX = start.Item1 + startCellX + centerCellX;
                float positionZ = start.Item2 + startCellZ + centerCellZ;

                float distanceStartY = 10f;
                Vector3 startArea = new Vector3(positionX, distanceStartY, positionZ);
                Vector3 target = new Vector3(positionX, map.transform.position.y - 1, positionZ);

                if (NavMesh.SamplePosition(startArea, out NavMeshHit hit, distanceStartY, areaMask))
                {
                    Vector3 cellWorldPosition = hit.position;
                    Vector2Int cellGridPosition = new Vector2Int(i, j);

                    Cell newCell = new Cell(cellWorldPosition, cellGridPosition);
                    grid[i, j] = newCell;

                    _allCells.Add(newCell);
                }
            }
        }

        return grid;
    }
}