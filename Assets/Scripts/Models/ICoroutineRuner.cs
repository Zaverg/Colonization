using System.Collections;
using UnityEngine;

public interface ICoroutineRuner 
{
    public Coroutine StartChildCoroutine(IEnumerator coroutine);
    public void StopChildCoroutine(Coroutine coroutine);
}