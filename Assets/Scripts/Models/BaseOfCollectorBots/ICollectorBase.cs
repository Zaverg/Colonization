public interface ICollectorBase
{
    public BaseMenu Stats { get; }
    public int CountResurceToCreateBot { get; }
    public int CountResurceToBuildBase { get; }
    public Timer Timer { get; }
    public ResourceCounter ResurceCounter { get; }

    public CollectorBotDispatcher BotDispatcher { get; }

    public void PlaceFlag(Flag flag);
}