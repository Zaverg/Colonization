using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private CollectorBotTaskName _taskName;
    private BuildProcess _buildProcess;

    public event Action<Flag> Activated;
    public event Action<CollectorBotTaskName> Installed;
    public event Action Deactivated;

    public BuildProcess BuildProcess => _buildProcess;

    public void Install(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        Installed?.Invoke(_taskName);
    }

    public void SetBuildProcess(BuildProcess process)
    {
        if (process != null)
            _buildProcess = process;
    }
     
    public void OnButtonClick(CollectorBotTaskName taskName)
    {
        _taskName = taskName;
        Activated?.Invoke(this);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;
        _buildProcess = null;

        Deactivated?.Invoke();
    }
}