using System;

public class BuildState : BotState
{
    private IBot _bot;
    public override event Action Completed;

    public override void Entry(IBot stateMachine)
    {
        _bot = stateMachine;
        _bot.Builder.StartBuild(_bot.CurrentTask.BuildProcess, _bot);
    }

    public override void Run()
    {
        
    }

    public override void Exit()
    {
        _bot = null;
    }
}
