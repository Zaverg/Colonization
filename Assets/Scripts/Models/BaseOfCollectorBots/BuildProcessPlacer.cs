using UnityEngine;

public class BuildProcessPlacer : MonoBehaviour
{
    [SerializeField] private Flag _activeFlag;
    [SerializeField] private BuildProcess _buildProcess;

    public void TryInstallFlag(Transform surface)
    {
        if (_activeFlag != null && surface.TryGetComponent<Map>(out _))
        {
            Vector3 installPosition = _buildProcess.Install();
            _activeFlag.Install(installPosition);
            _activeFlag.SetBuildProcess(_buildProcess);
            _activeFlag = null;
        }
        else if (_activeFlag != null)
        {
            _activeFlag.Deactivate();
            _activeFlag = null;
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

        _buildProcess = buildProcess;
    }
}