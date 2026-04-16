using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour, IGridOccupant, IResource
{
    [SerializeField] private MineralConfig _mineralConfig;

    public List<Vector2Int> _occupyArea = new List<Vector2Int>();
    public Transform Transform => transform;

    public abstract event Action<IResource> Took;
    public abstract event Action<IResource> Unlodered;
    public abstract event Action<IGridOccupant> OnFreeCells;

    public MineralConfig Config => _mineralConfig;

    public IReadOnlyList<Vector2Int> OccupyCells => _occupyArea;

    public void SetConfig(MineralConfig config)
    {
        if (config == null)
            return;

        _mineralConfig = config;

        GetComponent<MeshFilter>().mesh = _mineralConfig.Mesh;
        GetComponent<MeshRenderer>().material = _mineralConfig.Material;
    }

    public void SetOccupyArea(List<Vector2Int> area)
    {
        if (area == null || area.Count == 0)
            return;

        _occupyArea = area;
    }

    public void SetOccupyCell(Vector2Int position)
    {
        _occupyArea.Add(position);
    }

    public abstract void SetGridArea(List<Vector2Int> area);


    public abstract void SetGridPosition(Vector2Int gridPositon);

    public abstract void Take();

    public abstract void Drop();

    public abstract void ReturnToPool();

}