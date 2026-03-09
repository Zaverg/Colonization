using System;

public class BuildState : CollectorBotState
{
    private IStateMachine _stateMachine;
    public override event Action Completed;

    public override void Entry(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _stateMachine.Builder.Build(stateMachine.CurrentTask.Base)
    }

    public override void Run()
    {
        
    }

    public override void Exit()
    {
        
    }
}

public interface IBuild
{
    public void StartBuild();
}
