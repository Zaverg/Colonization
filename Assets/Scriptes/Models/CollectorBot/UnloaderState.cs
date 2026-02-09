using System;

public class UnloaderState : CollectorState
{
    private IStateMachine _stateMachine;

    public override event Action Completed;

    public override void Entry(IStateMachine stateMachine) 
    {
        _stateMachine = stateMachine;
        _stateMachine.Dropper.ReleaseResource();
    }

    public override void Run() 
    {
        if (_stateMachine.Dropper.IsStorageEmpty)
            Completed?.Invoke();
    }

    public override void Exit()
    {
        _stateMachine = null;
    }
}