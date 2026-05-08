using System;

public class ResourceCounter
{
    private int _collectedResources;

    public event Action<int> CountChanged;

    public int CollectedResources => _collectedResources;

    public void UpdateCounter(IResource resource)
    {
        resource.Unloaded -= UpdateCounter;

        _collectedResources++;
        CountChanged?.Invoke(_collectedResources);
    }

    public void Subtract(int count)
    {
        if (count <= 0 || count > _collectedResources)
            return;

        _collectedResources -= count;
        CountChanged?.Invoke(_collectedResources);
    }
}