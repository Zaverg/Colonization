using System;

public class IdleState : BotState
{
    private IBot _bot;

    public override event Action Completed;

    public override void Entry(IBot stateMachine) 
    { 
        _bot = stateMachine;
    }

    public override void Run()  { }

    public override void Exit() 
    {
        _bot = null;
    }
}