using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineralRegistry : MonoBehaviour
{
    private HashSet<IResource> _takenMineral = new HashSet<IResource>();
    private HashSet<IResource> _availableMinerals = new HashSet<IResource>();

    public int AvailableMineralsCount => _availableMinerals.Count;

    public void Register(IResource collectable)
    {
        if (_takenMineral.Contains(collectable) == false)
        {
            if (collectable.Transform.gameObject.activeSelf)
                _availableMinerals.Add(collectable);
        }
    }
   
    public IResource GetAvailableMineral()
    {
        IResource collectable = _availableMinerals.ElementAt(0);

        _availableMinerals.Remove(collectable);
        _takenMineral.Add(collectable);

        collectable.Unloaded += ReleaseMineral;

        return collectable;
    }

    public void ReleaseMineral(IResource collectable)
    {
        collectable.Unloaded -= ReleaseMineral;
        collectable.ReturnToPool();

        _takenMineral.Remove(collectable);
    }
}