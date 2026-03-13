using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flag : MonoBehaviour
{
    private bool _isFollower;

    public event Action<Flag> Activated;
    public event Action<CollectorBotTaskName> Installed;
    public event Action Deactivated;

    private CollectorBotTaskName _taskName;

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
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 56;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);
    }
}
