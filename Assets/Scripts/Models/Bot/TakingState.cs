using System;

public class TakingState : BotState
{
    private IBot _bot;

    public override event Action Completed;

    public override void Entry(IBot bot) 
    {
        _bot = bot;
        IResource resource = bot.CurrentTask.Resource;

        _bot.Taker.PlaceResourceInStorage(resource);
    }

    public override void Run() 
    {
        if (_bot.Taker.IsStorageFilled)
            Completed?.Invoke();
    }

    public override void Exit() 
    {
        _bot = null;
    }
}