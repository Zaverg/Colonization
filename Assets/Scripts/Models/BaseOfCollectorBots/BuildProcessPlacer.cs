using System.Collections.Generic;
using UnityEngine;

public class BuildProcessPlacer : MonoBehaviour
{
    [SerializeField] private CellRegister _cellRegister;

    [SerializeField] private Flag _activeFlag;
    [SerializeField] private BuildProcess _buildProcess;

    private List<Vector2Int> _occupyArea = new List<Vector2Int>();

    public void TryInstallFlag(Transform surface)
    {
        if (_occupyArea.Count == 0 || surface.TryGetComponent<Map>(out _) == false)
        {
            _buildProcess.PositionChanged -= CheckPosition;
            _buildProcess.Release();

            _activeFlag = null;
            _buildProcess = null;

            return;
        }

        Vector3 installPosition = _buildProcess.Install();
        _activeFlag.Install(installPosition);
        _activeFlag.SetBuildProcess(_buildProcess);

        _activeFlag = null;
        _buildProcess = null;
    }

    public void SetFlag(Flag flag)
    {
        if (flag == null)
            return;

        if (_activeFlag != null)
            _activeFlag.Deactivate();

        _activeFlag = flag;
    }

    public void SetBuilder(BuildProcess buildProcess)
    {
        if (buildProcess == null)
            return;

        if (_buildProcess != null)
        {
            Debug.Log("null");
            _buildProcess.PositionChanged -= CheckPosition;
            _buildProcess.Release();
        }

        _buildProcess = buildProcess;

        _buildProcess.PositionChanged += CheckPosition;
    }

    private void CheckPosition()
    {
        _occupyArea = _cellRegister.TryGetOccupyArea(new List<BuildingShapeUnit>(_buildProcess.Shapes));
        Debug.Log(_occupyArea.Count > 0);
    }
}