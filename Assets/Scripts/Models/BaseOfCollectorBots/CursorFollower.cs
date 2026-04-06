using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollower : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    private Camera _mainCamera;
    private Mouse _mouse;

    public void Awake()
    {
        _mainCamera = Camera.main;
        _mouse = Mouse.current;
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
        Vector3 mousePosition = _mouse.position.ReadValue();
        mousePosition.z = 56;
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);
        Vector2Int gridPosition = _grid.ConvertWorldToGridPosition(worldPosition);

        Debug.Log(gridPosition);

        if ((gridPosition.x >= 0 && gridPosition.y >= 0) && (gridPosition.x < _grid.RowsGrid && gridPosition.y < _grid.ColumnsGrid))
            transform.position = _grid.GetCell(gridPosition.x, gridPosition.y).WorldPosition;
    }
}