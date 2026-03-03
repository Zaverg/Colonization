using System.Collections;
using UnityEngine;

public interface ICoroutineRunner 
{
    public Coroutine StartChildCoroutine(IEnumerator coroutine);
    public void StopChildCoroutine(Coroutine coroutine);
}