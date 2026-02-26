using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.ProBuilder;

[RequireComponent(typeof(NavMeshSurface))]
public class Map : MonoBehaviour
{
    private const int BasePlaneScale = 10;

    [SerializeField] private ProBuilderMesh _proBuilderMesh;

    private float _halfScaleMapX;
    private float _halfScaleMapZ;

    public float HalfScaleMapX => _halfScaleMapX;
    public float HalfScaleMapZ => _halfScaleMapZ;

    public void Initialize()
    {
        if (_proBuilderMesh == null)
            return;

        Mesh mesh = _proBuilderMesh.GetComponent<MeshFilter>().sharedMesh;
        Vector3 scale = mesh.bounds.size;

        _halfScaleMapX = scale.x * BasePlaneScale / 2;
        _halfScaleMapZ = scale.z * BasePlaneScale / 2;
    }
}