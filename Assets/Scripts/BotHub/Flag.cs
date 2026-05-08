using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private BuildProcess _buildProcess;

    public event Action Installed;
    public event Action Deactivated;

    public BuildProcess BuildProcess => _buildProcess;

    public void Install(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        Installed?.Invoke();
    }

    public void SetBuildProcess(BuildProcess process)
    {
        if (process != null)
            _buildProcess = process;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;
        _buildProcess = null;

        Deactivated?.Invoke();
    }
}