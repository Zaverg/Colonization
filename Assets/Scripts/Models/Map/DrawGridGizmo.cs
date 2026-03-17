using UnityEngine;

public class DrawGridGizmo : MonoBehaviour
{
    [SerializeField] private Map _map;

    private void OnDrawGizmos()
    {
        Vector3 startGrid = new Vector3(_map.transform.position.x - _map.transform.localScale.x * 5, transform.position.y, _map.transform.position.z - _map.transform.localScale.z * 5);
        Vector3 endGrid = new Vector3(_map.transform.position.x + _map.transform.localScale.x * 5, transform.position.y, _map.transform.position.z + _map.transform.localScale.z * 5);

        int rows = Mathf.CeilToInt(endGrid.x - startGrid.x);
        int columns = Mathf.CeilToInt(endGrid.z - startGrid.z);

        Gizmos.color = Color.green;

        for (int i = 0; i < rows + 1; i++)
        {
            Vector3 start = startGrid + new Vector3(i, 0, 0);
            Vector3 end = startGrid + new Vector3(i, 0, columns);

            Gizmos.DrawLine(start, end);
        }

        for (int i = 0; i < columns + 1; i++)
        {
            Vector3 start = startGrid + new Vector3(0, 0, i);
            Vector3 end = startGrid + new Vector3(rows, 0, i);

            Gizmos.DrawLine(start, end);
        }
    }
}