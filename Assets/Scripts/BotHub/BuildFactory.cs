using System.Collections.Generic;
using UnityEngine;

public abstract class BuildFactory : MonoBehaviour
{
    public abstract Building Create(Vector3 position, List<Vector2Int> gridPosition);
}