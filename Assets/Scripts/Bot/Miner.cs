using UnityEngine;

public class Miner : MonoBehaviour
{
    [SerializeField] private CoroutineRunner _coroutineRunner;

    private Timer _timer;

    public bool HasMined => _timer.IsComplete;

    public void Awake()
    {
        _timer = new Timer(_coroutineRunner);
    }

    public void SetDuration(float duration)
    {
        _timer.SetDuration(duration);
    }

    public void StartMining()
    {
        _timer.Run();
    }

    public void SetCoroutineRunner(CoroutineRunner coroutineRunner)
    {
        if (coroutineRunner == null)
            return;

        _coroutineRunner = coroutineRunner;

        _timer = new Timer(_coroutineRunner);
    }
}
