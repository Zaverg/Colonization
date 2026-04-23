using System.Collections;
using UnityEngine;

public interface ICoroutineRunner 
{
    public Coroutine StartCoroutine(IEnumerator coroutine);
    public void StopCoroutine(Coroutine coroutine);
}