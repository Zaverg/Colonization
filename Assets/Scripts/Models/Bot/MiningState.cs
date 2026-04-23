using System;

public class MiningState : BotState
{
    private IBot _bot;

    public override event Action Completed;

    public override void Entry(IBot bot)
    {
        _bot = bot;

        IResource resource = _bot.CurrentTask.Resource;
        float duration = resource.Config.MiningDuration;

        _bot.Miner.SetDuration(duration);
        _bot.Miner.StartMining();
    }

    public override void Run() 
    {
        if (_bot.Miner.HasMined)
            Completed?.Invoke();
    }

    public override void Exit()
    {
        _bot = null;
    }
}
