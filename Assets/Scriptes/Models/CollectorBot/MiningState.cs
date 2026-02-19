using System;

public class MiningState : CollectorBotState
{
    private IStateMachine _stateMachine;

    public override event Action Completed;

    public override void Entry(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        IResource collectable = _stateMachine.CurrentTask.Mineral;
        float duration = collectable.Config.MiningDuration;

        _stateMachine.Miner.SetDuration(duration);
        _stateMachine.Miner.StartMining();
    }

    public override void Run() 
    {
        if (_stateMachine.Miner.HasMined)
            Completed?.Invoke();
    }

    public override void Exit()
    {
        _stateMachine = null;
    }
}
