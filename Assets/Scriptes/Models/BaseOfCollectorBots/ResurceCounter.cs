using System;
using UnityEngine;

public class ResurceCounter : MonoBehaviour
{
    private int _collectedResources;

    public event Action<int> MineralCountChanged;

    public void UpdateCounter(IResource collectable)
    {
        collectable.Dropped -= UpdateCounter;

        _collectedResources++;
        MineralCountChanged?.Invoke(_collectedResources);
        
    }
}