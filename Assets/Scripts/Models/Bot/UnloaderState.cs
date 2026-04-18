using System;

public class UnloaderState : CollectorBotState
{
    private IStateMachine _stateMachine;

    public override event Action Completed;

    public override void Entry(IStateMachine stateMachine) 
    {
        _stateMachine = stateMachine;
        _stateMachine.Unloader.ReleaseResource();
    }

    public override void Run() 
    {
        if (_stateMachine.Unloader.IsStorageEmpty)
            Completed?.Invoke();
    }

    public override void Exit()
    {
        _stateMachine = null;
    }
}