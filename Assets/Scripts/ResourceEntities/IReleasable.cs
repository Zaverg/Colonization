using UnityEngine;
using System;

public interface IReleasable<T> where T : Component
{
    public event Action<T> Released;
}