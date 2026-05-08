using System;

public abstract class BaseState
{
    public abstract event Action<Type> Completed;

    public abstract void Entry(IBotHub collectorBase);
    public abstract void Run();
    public abstract void Exit();
}
