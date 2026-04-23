using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
{
    public Coroutine StartCoroutine(IEnumerator coroutine)
    {
        if (coroutine == null)
            return null;

        return base.StartCoroutine(coroutine);
    }

    public void StopCoroutine(Coroutine coroutine)
    {
        if (coroutine == null)
            return;

        base.StopCoroutine(coroutine);
    }
}
