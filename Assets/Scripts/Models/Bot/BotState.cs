using System;

public abstract class BotState
{
    public abstract event Action Completed;

    public abstract void Entry(IBot stateMachine);
    public abstract void Run();
    public abstract void Exit();
}
