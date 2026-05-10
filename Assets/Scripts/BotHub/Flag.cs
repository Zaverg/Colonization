using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private BuildProcess _buildProcess;

    public event Action Installed;
    public event Action Deactivated;

    public BuildProcess BuildProcess => _buildProcess;

    public void Install(BuildProcess buildProcess)
    {
        gameObject.SetActive(true);
        _buildProcess = buildProcess;
        transform.position = _buildProcess.transform.position;
       
        Installed?.Invoke();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;
        _buildProcess = null;

        Deactivated?.Invoke();
    }
}