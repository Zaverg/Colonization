using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flag : MonoBehaviour
{
    private bool _isFollower;
    private CollectorBotTaskName _taskName;

    private Camera _mainCamera;
    private Mouse _mouse;

    public event Action<Flag> Activated;
    public event Action<CollectorBotTaskName> Installed;
    public event Action Deactivated;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _mouse = Mouse.current;
    }

    private void Update()
    {
        if (_isFollower == false)
            return;

       FollowCursor();
    }

    public void Instal()
    {
        _isFollower = false;
        Installed?.Invoke(_taskName);
    }

    public void OnButtonClick(CollectorBotTaskName taskName)
    {
        _taskName = taskName;

        if (gameObject.activeSelf)
            Deactivate();
        else
            Activate();
    }

    public void Activate()
    {
        _isFollower = true;
        gameObject.SetActive(true);
        Activated?.Invoke(this);
    }

    public void Deactivate()
    {
        _isFollower = false;
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;

        Deactivated?.Invoke();
    }

    private void FollowCursor()
    {
        Vector3 mousePosition = _mouse.position.ReadValue();
        mousePosition.z = 56;
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);
    }
}

public class GridMover : MonoBehaviour
{
    
}

public class GridPositionColculator
{
    private IReadOnlyList<Cell> _cells;
    
    public GridPositionColculator(IReadOnlyList<Cell> cells)
    {
        _cells = cells;
    }

    public Vector3 GetCenterCellWorldPosition(Vector3 position)
    {
        Vector3 cellPosition = new Vector3();

        float minDistance = float.MaxValue;

        foreach (Cell cell in _cells)
        {
            float currentDistance = Vector3.Distance(cell.WorldPosition, position);

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                cellPosition = cell.WorldPosition;
            }
        }

        return cellPosition;
    }
}