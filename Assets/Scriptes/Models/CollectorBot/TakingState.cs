using System;

public class TakingState : State
{
    private IStateMachine _stateMachine;

    public override event Action Completed;

    public override void Entry(IStateMachine stateMachine) 
    {
        _stateMachine = stateMachine;
        IResource collectable = stateMachine.CurrentTask.Mineral;

        _stateMachine.Taker.PlaceResourceInStorage(collectable);
    }

    public override void Run() 
    {
        if (_stateMachine.Taker.IsStorageFilled)
            Completed?.Invoke();
    }

    public override void Exit() 
    {
        _stateMachine = null;
    }
}