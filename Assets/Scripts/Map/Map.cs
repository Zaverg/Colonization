using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class Map : MonoBehaviour
{
    private const int HalfBasePlaneScale = 5;

    public float HalfScaleMapX { get; private set; }
    public float HalfScaleMapZ { get; private set; }

    public void Initialize()
    {
        HalfScaleMapX = transform.localScale.x * HalfBasePlaneScale;
        HalfScaleMapZ = transform.localScale.z * HalfBasePlaneScale;  
    }
}