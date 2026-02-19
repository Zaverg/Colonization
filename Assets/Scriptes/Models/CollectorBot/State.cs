using System;

public abstract class State
{
    public abstract event Action Completed;
    public abstract void Entry();
    public abstract void Run();
    public abstract void Exit();
}