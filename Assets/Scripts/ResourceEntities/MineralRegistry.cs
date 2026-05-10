using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineralRegistry : MonoBehaviour
{
    private HashSet<IResource> _takenMineral = new HashSet<IResource>();
    private HashSet<IResource> _availableMinerals = new HashSet<IResource>();

    public int AvailableMineralsCount => _availableMinerals.Count;

    public void Register(IResource mineral)
    {
        if (_takenMineral.Contains(mineral) == false)
        {
            if (mineral.Transform.gameObject.activeSelf)
                _availableMinerals.Add(mineral);
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

    public void ReleaseMineral(IResource mineral)
    {
        mineral.Unloaded -= ReleaseMineral;
        mineral.OnRelease();

        _takenMineral.Remove(mineral);
    }
}