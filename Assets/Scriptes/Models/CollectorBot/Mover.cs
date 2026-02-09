using System;
using UnityEngine;

public abstract class Mover : MonoBehaviour, IMover
{
    public abstract void SetTarget(Vector3 target);

    public abstract void Move();

    public abstract bool HasReachedTarget();
}