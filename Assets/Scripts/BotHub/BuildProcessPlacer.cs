using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildProcessPlacer : MonoBehaviour
{
    [SerializeField] private CellRegister _cellRegister;

    [SerializeField] private Flag _flag;
    [SerializeField] private BuildProcess _buildProcess;
    [SerializeField] private BuildProcessSpawner _buildProcessSpawner;
    [SerializeField] private BaseMenu _baseMenu;

    public void TryInstallFlag(Transform surface)
    {
        if (_buildProcess.OccupyArea.Count == 0 || surface.TryGetComponent<Map>(out _) == false)
        {
            _buildProcess.Release();
            _buildProcess = null;

            return;
        }

        _buildProcess.Completed += CompelatedBuild;

        Vector3 position = _buildProcess.Install();
        _cellRegister.ReserveArea(_buildProcess.OccupyArea.ToList());

        _flag = _baseMenu.CurrentBase.Flag;
        _flag.Install(position);
        _flag.SetBuildProcess(_buildProcess);

        _buildProcess.PositionChanged -= CheckPosition;
    }

    public void SpawnBuilder(BuildType type)
    {
        if (_flag != null)
        {
            _cellRegister.FreeCells(_buildProcess.OccupyArea.ToList());
            _flag.Deactivate();
            _flag = null;
            _cellRegister.FreeCells(_buildProcess.OccupyArea.ToList());

            if (_buildProcess.BuilderType == type)
            {
                _buildProcess.Interrupt();
                _buildProcess.PositionChanged += CheckPosition;

                return;
            }
        }

        if (_buildProcess != null)
            _buildProcess.PositionChanged -= CheckPosition;

        _buildProcess = _buildProcessSpawner.Spawn(type);
        _buildProcess.PositionChanged += CheckPosition;
    }

    private List<Vector2Int> CheckPosition()
    {
        return _cellRegister.TryGetOccupyArea(new List<BuildingShapeUnit>(_buildProcess.Shapes)); ;
    }

    public void CompelatedBuild(Building building, IStateMachine stateMachine)
    {
        _buildProcess.Completed -= CompelatedBuild;
        _cellRegister.OccupyArea(building);

        _buildProcess = null;
        _flag = null;
    }
}