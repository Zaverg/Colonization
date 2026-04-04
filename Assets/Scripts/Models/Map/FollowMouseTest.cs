using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;

public class FollowMouseTest : MonoBehaviour
{
    private Camera _camera;
    private Mouse _mouse;

    private void Awake()
    {
        _camera = Camera.main;
        _mouse = Mouse.current;
    }

    private void Update()
    {
        Vector3 mousePosition = _mouse.position.ReadValue();
        mousePosition.z = 56;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);

        List<BuildingShapeUnit> shapes = GetComponentsInChildren<BuildingShapeUnit>().ToList();
        List<Vector3> allPosition = shapes.Select(shape => shape.transform.position).ToList();

        transform.position = GetSnappedCenterPosition(allPosition);
    }

    private Vector3 GetSnappedCenterPosition(List<Vector3> allBuildingPosition)
    {
        List<int> xs = allBuildingPosition.Select(position => Mathf.FloorToInt(position.x)).ToList();
        List<int> zs = allBuildingPosition.Select(position => Mathf.FloorToInt(position.z)).ToList();

        float centerX = (xs.Min() + xs.Max()) / 2f + 1 / 2f;
        float centerZ = (zs.Min() + zs.Max()) / 2f + 1 / 2f;

        return new Vector3(centerX, 0, centerZ);
    }
}