using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollower : MonoBehaviour
{
    private Camera _mainCamera;
    private Mouse _mouse;

    private List<BuildingShapeUnit> _buildingShapes;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _mouse = Mouse.current;

        _buildingShapes = GetComponentsInChildren<BuildingShapeUnit>().ToList();
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 mousePosition = _mouse.position.ReadValue();
        mousePosition.z = 56;
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);
        
        if (_buildingShapes.Count > 0)
        {
            List<Vector3> positions = _buildingShapes.Select(shape => shape.transform.position).ToList();
            transform.position = GetSnappedCenterPosition(positions);
        }
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
