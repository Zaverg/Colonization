using UnityEngine;

public class CollectorBotMiner : Miner
{
    [SerializeField] private CoroutineRunner _coroutineRunner;

    private Timer _timer;

    public override bool HasMined => _timer.IsComplete;

    public void Awake()
    {
        _timer = new Timer(_coroutineRunner);
    }

    public override void SetDuration(float duration)
    {
        _timer.SetDuration(duration);
    }

    public override void StartMining()
    {
        _timer.Run();
    }

    public void SetCorutineRunner(CoroutineRunner coroutineRunner)
    {
        if (coroutineRunner == null)
            return;

        _coroutineRunner = coroutineRunner;

        _timer = new Timer(_coroutineRunner);
    }
}
