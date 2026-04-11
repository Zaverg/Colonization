using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour, IGridOccupant, IResource
{
    [SerializeField] private MineralConfig _mineralConfig;

    public List<Cell> _occupyCells = new List<Cell>();
    public Transform Transform => transform;

    public abstract event Action<IResource> Took;
    public abstract event Action<IResource> Unlodered;
    public abstract event Action<IGridOccupant> OnGridOut;

    public MineralConfig Config => _mineralConfig;

    public IReadOnlyList<Cell> OccupyCells => _occupyCells;

    public void SetConfig(MineralConfig config)
    {
        if (config == null)
            return;

        _mineralConfig = config;

        GetComponent<MeshFilter>().mesh = _mineralConfig.Mesh;
        GetComponent<MeshRenderer>().material = _mineralConfig.Material;
    }

    public void SetGridPosition(List<Cell> cells)
    {
        if (cells == null || cells.Count == 0)
            return;

        _occupyCells = cells;
    }

    public abstract void Take();

    public abstract void Drop();

    public abstract void ReturnToPool();
}