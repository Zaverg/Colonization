using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineralRegistry : MonoBehaviour
{
    private HashSet<IResource> _occupiedMinerals = new HashSet<IResource>();
    private HashSet<IResource> _availableMinerals = new HashSet<IResource>();

    public int AvailableMineralsCount => _availableMinerals.Count;

    public void Register(IResource collectable)
    {
        if (_occupiedMinerals.Contains(collectable) == false)
        {
            if (collectable.Transform.gameObject.activeSelf)
                _availableMinerals.Add(collectable);
        }
    }

    public IResource GetAvailableMineral()
    {
        IResource collectable = _availableMinerals.ElementAt(0);

        _availableMinerals.Remove(collectable);
        _occupiedMinerals.Add(collectable);

        collectable.Unlodered += RemoveMineral;

        return collectable;
    }

    public void RemoveMineral(IResource collectable)
    {
        collectable.Unlodered -= RemoveMineral;
        collectable.ReturnToPool();

        _occupiedMinerals.Remove(collectable);
    }
}