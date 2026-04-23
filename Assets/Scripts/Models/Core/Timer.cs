using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private float _duration;
    private float _currentSeconds;

    private Coroutine _coroutine;
    private ICoroutineRunner _coroutineRunner;
    
    public event Action Ended;
    public event Action<float> Changed;

    public Timer(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public float CurrentSeconds => _currentSeconds;
    public bool IsComplete { get; private set; }

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
            _coroutineRunner.StopCoroutine(_coroutine);

        _currentSeconds = _duration;
        IsComplete = false;

        _coroutine = _coroutineRunner.StartCoroutine(StartTimer());
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

        IsComplete = true;

        Ended?.Invoke();
    }
}