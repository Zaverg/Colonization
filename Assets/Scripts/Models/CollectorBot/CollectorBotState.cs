using System;

public abstract class CollectorBotState
{
    public abstract event Action Completed;

    public abstract void Entry(IStateMachine stateMachine);
    public abstract void Run();
    public abstract void Exit();
}
