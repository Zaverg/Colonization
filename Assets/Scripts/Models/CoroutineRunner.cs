using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour, ICoroutineRuner
{
    public Coroutine StartChildCoroutine(IEnumerator coroutine)
    {
        if (coroutine == null)
            return null;

        return StartCoroutine(coroutine);
    }

    public void StopChildCoroutine(Coroutine coroutine)
    {
        if (coroutine == null)
            return;

        StopCoroutine(coroutine);
        coroutine = null;
    }
}
