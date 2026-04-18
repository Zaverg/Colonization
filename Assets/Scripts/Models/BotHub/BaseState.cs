using System;

public abstract class BaseState
{
    public abstract event Action Completed;

    public abstract void Entry(ICollectorBase collectorBase);
    public abstract void Run();
    public abstract void Exit();
}
