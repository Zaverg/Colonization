using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flag : MonoBehaviour, IClickable
{
    private bool _isPut;

    public event Action Installed; 

    private void Update()
    {
        if (_isPut)
            return;

       FollowCursor();
    }

    public void OnClick()
    {
        _isPut = true;
        Installed?.Invoke();
    }

    public void Activate()
    {
        _isPut = false;
        gameObject.SetActive(true);
    }

    public void Deactivate(IBuild build)
    {
        gameObject.SetActive(false);
        build.OnEndBuild -= Deactivate;
    }

    private void FollowCursor()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 56;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);
    }
}