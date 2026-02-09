using System;

public abstract class CollectorState
{
    public abstract event Action Completed;
    public abstract void Entry(IStateMachine stateMachine);
    public abstract void Run();
    public abstract void Exit();
}