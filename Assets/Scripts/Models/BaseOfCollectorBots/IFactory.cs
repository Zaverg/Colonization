using UnityEngine;

public interface IFactory
{
    public ICreatable Create(Vector3 position, bool visible);
}
