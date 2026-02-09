using UnityEngine;

public class CollectorBotMiner : Miner
{
    [SerializeField] private CoroutineRunner _runer;

    private Timer _timer;

    public override bool HasMined => _timer.IsComplete;

    public void Awake()
    {
        _timer = new Timer(_runer);
    }

    public override void SetDuration(float duration)
    {
        _timer.SetDuration(duration);
    }

    public override void StartMining()
    {
        _timer.Run();
    }
}
