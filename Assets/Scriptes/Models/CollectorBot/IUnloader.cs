public interface IUnloader
{
    public bool IsStorageEmpty { get; }
    public IResource ReleaseResource();
}
