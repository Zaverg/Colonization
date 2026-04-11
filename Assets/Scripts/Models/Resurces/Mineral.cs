using System;
using UnityEngine;

public class Mineral : MonoBehaviour, IReleasable<Mineral>, IResource, IGridOccupant
{
    [SerializeField] private MineralConfig _mineralConfig;

    public event Action<Mineral> Released;
    public event Action<IResource> Taked;
    public event Action<IResource> Unlodered;
    public event Action<IGridOccupant> OnGridOut;

    public Transform Transform => transform;
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
        Taked?.Invoke(this);
        OnGridOut?.Invoke(this);
    }

    public void Drop()
    {
        Unlodered?.Invoke(this);
    }

    public void ReturnToPool()
    {
        Released?.Invoke(this);
    }
}
