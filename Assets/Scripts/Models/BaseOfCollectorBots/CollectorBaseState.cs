using System;

public abstract class CollectorBaseState
{
    public abstract event Action Completed;

    public abstract void Entry(ICollectorBase collectorBase);
    public abstract void Run();
    public abstract void Exit();
}
