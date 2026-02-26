using System;

public class ResourceCounter
{
    private int _collectedResources;

    public event Action<int> MineralCountChanged;

    public int CollectedResources => _collectedResources;

    public void UpdateCounter(IResource collectable)
    {
        collectable.Unlodered -= UpdateCounter;

        _collectedResources++;
        MineralCountChanged?.Invoke(_collectedResources);
    }

    public void UpdateCounter(int count)
    {
        if (count <= 0)
            return;

        _collectedResources -= count;
        MineralCountChanged?.Invoke(_collectedResources);
    }
}