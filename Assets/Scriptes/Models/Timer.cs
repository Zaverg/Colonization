using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private float _duration;
    private float _currentSeconds;

    private bool _isComplete = false;
    private Coroutine _coroutine;
    private ICoroutineRuner _coroutineRuner;
    
    public event Action Ended;
    public event Action<float> Changed;

    public Timer(ICoroutineRuner corutineRuner)
    {
        _coroutineRuner = corutineRuner;
    }

    public bool IsComplete => _isComplete;

    public void SetDuration(float duration) 
    {
        if (duration <= 0)
            return;

        _duration = duration;
    }

    public void Run()
    {
        if (_duration == 0)
            return;

        if (_coroutine != null)
            _coroutineRuner.StopChildCoroutine(_coroutine);

        _currentSeconds = _duration;
        _isComplete = false;

        _coroutine = _coroutineRuner.StartChildCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        float lastUpdateTime = _currentSeconds;
        float intervalUpdateUI = 0.9f;

        while(_currentSeconds > 0)
        {
            _currentSeconds -= Time.deltaTime;

            if (lastUpdateTime - _currentSeconds >= intervalUpdateUI)
            {
                Changed?.Invoke(_currentSeconds);
                lastUpdateTime = _currentSeconds;
            }

            yield return null;
        }

        _isComplete = true;

        Ended?.Invoke();
    }
}