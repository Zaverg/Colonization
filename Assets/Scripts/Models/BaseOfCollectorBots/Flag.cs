using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flag : MonoBehaviour, IClickable
{
    private bool _isPut;

    public event Action<Flag> Installed;

    private void Update()
    {
        if (_isPut)
            return;

       FollowCursor();
    }

    public void OnClick()
    {
        Put();
    }

    private void FollowCursor()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 56;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);
    }

    private void Put()
    {
        _isPut = true;
        Installed?.Invoke(this);
    }
}
