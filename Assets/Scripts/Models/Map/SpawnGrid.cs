using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnGrid : MonoBehaviour
{
    [SerializeField] private Map _map;

    private HashSet<Cell> _freeCells = new HashSet<Cell>();
    private HashSet<Cell> _occupiedCells = new HashSet<Cell>();

    private Dictionary<IResource, Cell> _mineralsToCells = new Dictionary<IResource, Cell>();

    private GridCreator _gridCreator;

    public void Initialize()
    {
        _gridCreator = new GridCreator();
        _gridCreator.Create(_map);

        _freeCells = new HashSet<Cell>(_gridCreator.AllCells);

        gameObject.SetActive(true);
    }

    public void OccupyCell(IResource mineral)
    {
        mineral.Taked += OnMineralTaked;
     
        int index = Random.Range(0, _freeCells.Count);

        Cell cell = _freeCells.ElementAt(index);
        _mineralsToCells[mineral] = cell;

        mineral.Transform.position = cell.WorldPosition;

        _freeCells.Remove(cell);
        _occupiedCells.Add(cell);
    }
   
    private void OnMineralTaked(IResource collectable)
    {
        collectable.Taked -= OnMineralTaked;

        Cell cell = _mineralsToCells[collectable];

        _occupiedCells.Remove(cell);
        _freeCells.Add(cell);

        _mineralsToCells.Remove(collectable);
    }
}
