using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    [SerializeField] private IGrid _grid;

    private RayShooter _rayShooter;

    public void Awake()
    {
        _rayShooter = new RayShooter();
    }

    private void Update()
    {
        if (_grid == null) return;

        Follow();
    }

    public void SetGrid(IGrid grid)
    {
        if (grid == null) return;

        _grid = grid;
    }

    private void Follow()
    {
        if (_rayShooter.RaycastWorld(out RaycastHit hit))
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