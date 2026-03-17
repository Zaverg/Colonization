using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

public interface IGrid
{

}