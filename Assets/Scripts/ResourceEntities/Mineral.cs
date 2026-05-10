using System;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour, IReleasable<Mineral>, IGridOccupant, IResource
{
    [SerializeField] private MineralConfig _mineralConfig;

    private List<Vector2Int> _occupyArea = new List<Vector2Int>();

    public event Action<Mineral> Released;
    public event Action<IResource> Took;
    public event Action<IResource> Unloaded;
    public event Action<IGridOccupant> ReleasedCells;

    public Transform Transform => transform;

    public IReadOnlyList<Vector2Int> OccupyCells => _occupyArea;

    public MineralConfig Config => _mineralConfig;

    public void SetConfig(MineralConfig config)
    {
        if (config == null)
            return;

        _mineralConfig = config;

        GetComponent<MeshFilter>().mesh = _mineralConfig.Mesh;
        GetComponent<MeshRenderer>().material = _mineralConfig.Material;
    }

    public void Take()
    {
        Took?.Invoke(this);
        ReleasedCells?.Invoke(this);
    }

    public void Drop()
    {
        Unloaded?.Invoke(this);
    }

    public void OnRelease()
    {
        _occupyArea.Clear();
        Released?.Invoke(this);
    }

    public void SetGridArea(List<Vector2Int> area)
    {
        if (area == null || area.Count == 0)
            return;

        _occupyArea = new List<Vector2Int>(area);
    }

    public void SetGridPosition(Vector2Int gridPositon)
    {
        _occupyArea.Add(gridPositon);
    }
}
