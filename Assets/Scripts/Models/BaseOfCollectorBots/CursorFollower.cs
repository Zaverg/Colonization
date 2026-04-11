using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollower : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    private Camera _mainCamera;
    private Mouse _mouse;
    private RayShooter _rayShooter;

    public void Awake()
    {
        _mainCamera = Camera.main;
        _mouse = Mouse.current;
        _rayShooter = new RayShooter();
    }

    private void Update()
    {
        if (_grid == null) return;

        Follow();
    }

    public void SetGrid(Grid grid)
    {
        if (grid == null) return;

        _grid = grid;
    }

    private void Follow()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        Debug.Log("Hit 1" + mousePosition);

        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            Vector3 worldPosition = hit.point;


            transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
            Vector2Int gridPosition = _grid.ConvertWorldToGridPosition(worldPosition);

            if (_grid.IsInGrid(gridPosition))
            {
                transform.position = _grid.GetCell(gridPosition.x, gridPosition.y).WorldPosition;
            }
        }
    }
}