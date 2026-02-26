using UnityEngine;

public interface IMover
{
    public void SetTarget(Vector3 target);
    public void Move();
    public bool HasReachedTarget();
}