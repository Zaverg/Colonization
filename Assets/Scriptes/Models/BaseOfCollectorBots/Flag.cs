using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flag : MonoBehaviour
{
    public event Action<Transform> Installed;

    private void Update()
    {
       FollowCursor();
    }

    private void FollowCursor()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 56;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);
    }
}
