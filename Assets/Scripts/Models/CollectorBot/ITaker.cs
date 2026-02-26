public interface ITaker
{
    public bool IsStorageFilled { get; }
    public void PlaceResourceInStorage(IResource collectable);
}