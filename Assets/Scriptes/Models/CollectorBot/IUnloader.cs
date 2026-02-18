using System;

public interface IUnloader
{
    public event Action<IResource> Unloaded;
    public bool IsStorageEmpty { get; }
    public IResource ReleaseResource();
}
