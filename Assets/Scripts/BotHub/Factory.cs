using UnityEngine;

public abstract class Factory : MonoBehaviour, IFactory
{
    public abstract Building Create(Vector3 position, bool visible);
}