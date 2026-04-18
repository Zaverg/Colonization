using System;

public class BuildState : CollectorBotState
{
    private IStateMachine _stateMachine;
    public override event Action Completed;

    public override void Entry(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _stateMachine.Builder.StartBuild(_stateMachine.CurrentTask.BuildProcess, _stateMachine);
    }

    public override void Run()
    {
        
    }

    public override void Exit()
    {
        _stateMachine = null;
    }
}
