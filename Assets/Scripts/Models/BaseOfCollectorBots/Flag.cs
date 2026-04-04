using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CursorFollower))]
public class Flag : MonoBehaviour
{
    private bool _isFollower;
    private CollectorBotTaskName _taskName;

    private CursorFollower _cursorFollower;

    public event Action<Flag> Activated;
    public event Action<CollectorBotTaskName> Installed;
    public event Action Deactivated;

    private void Awake()
    {
        _cursorFollower = GetComponent<CursorFollower>();
    }

    private void Update()
    {
        _cursorFollower.enabled = _isFollower;
    }

    public void Install()
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
}

public class GridMover : MonoBehaviour
{
    
}