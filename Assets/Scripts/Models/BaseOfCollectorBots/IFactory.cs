using UnityEngine;

public interface IFactory
{
    public Building Create(Vector3 position, bool visible);
}
