using UnityEngine;

public class BuildProcessPlacer : MonoBehaviour
{
    [SerializeField] private Flag _activeFlag;
    [SerializeField] private BuildProcess _buildProcess;

    public void TryInstallFlag(Transform surface)
    {
        if (surface != null && surface.TryGetComponent<Map>(out _) && _activeFlag != null)
        {
            Vector3 installPosition = _buildProcess.Install();
            _activeFlag.Install(installPosition);
            _activeFlag.SetBuildProcess(_buildProcess);
        }
        else if (_activeFlag != null)
        {
            ResetData();
        }
    }

    public void SetFlag(Flag flag)
    {
        if (flag == null)
            return;

        _activeFlag = flag;
    }

    public void SetBuilder(BuildProcess buildProcess)
    {
        if (buildProcess == null)
            return;

        if (_buildProcess != null)
        {
            _buildProcess.Release();
        }

        _buildProcess = buildProcess;
    }

    private void ResetData()
    {
        _buildProcess.Release();
        _buildProcess = null;

        _activeFlag.Deactivate();
        _activeFlag = null;
    }
}