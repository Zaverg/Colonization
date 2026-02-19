using System;

public class IdleState : CollectorBotState
{
    private IStateMachine _stateMachine;

    public override event Action Completed;

    public override void Entry(IStateMachine stateMachine) 
    { 
        _stateMachine = stateMachine;
    }

    public override void Run()  { }

    public override void Exit() 
    {
        _stateMachine = null;
    }
}