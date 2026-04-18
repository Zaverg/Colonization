using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class Map : MonoBehaviour
{
    private const int BasePlaneScale = 10;

    public float HalfScaleMapX { get; private set; }
    public float HalfScaleMapZ { get; private set; }

    public void Initialize()
    {
        HalfScaleMapX = transform.localScale.x * BasePlaneScale / 2;
        HalfScaleMapZ = transform.localScale.z * BasePlaneScale / 2;  
    }
}