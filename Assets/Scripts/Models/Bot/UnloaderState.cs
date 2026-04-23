using System;

public class UnloaderState : BotState
{
    private IBot _bot;

    public override event Action Completed;

    public override void Entry(IBot stateMachine) 
    {
        _bot = stateMachine;
        _bot.Unloader.ReleaseResource();
    }

    public override void Run() 
    {
        if (_bot.Unloader.IsStorageEmpty)
            Completed?.Invoke();
    }

    public override void Exit()
    {
        _bot = null;
    }
}