using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildProcessPlacer : MonoBehaviour
{
    [SerializeField] private CellRegister _cellRegister;

    [SerializeField] private Flag _flag;
    [SerializeField] private BuildProcess _buildProcess;
    [SerializeField] private BuildProcessSpawner _buildProcessSpawner;
    [SerializeField] private BaseMenu _baseMenu;

    private List<Vector2Int> _occupyArea = new List<Vector2Int>();

    public void TryInstallFlag(Transform surface)
    {
        if (_occupyArea.Count == 0 || surface.TryGetComponent<Map>(out _) == false)
        {

            return;
        }

        _flag = _baseMenu.CurrentBase.Flag;
        _buildProcess.Started += Reseat;
        Vector3 position = _buildProcess.Install();
        _flag.Install(position);
        _flag.SetBuildProcess(_buildProcess);

        _buildProcess.PositionChanged -= CheckPosition;
    }

    public void SetBuilder(BuildType type)
    {
        if (_buildProcess != null)
        {
            // Если строитель еше не установлен и нужен такой же строитель
            if (_buildProcess.TypeBuilder == type && _flag == null)
                return;

            // Если строитель установлен, но стройка еше не начелась и нужен тотже строитель
            if (_buildProcess.TypeBuilder == type && _flag != null)
            {
                _buildProcess.Interrupt();
                _flag.Deactivate();

                return;
            }

            // Если нужен другой строитель
            _buildProcess.PositionChanged -= CheckPosition;
            _buildProcess.Release();
        }
        
        _buildProcess = _buildProcessSpawner.Spawn(type);
        _buildProcess.PositionChanged += CheckPosition;
    }

    private void CheckPosition()
    {
        _occupyArea = _cellRegister.TryGetOccupyArea(new List<BuildingShapeUnit>(_buildProcess.Shapes));
    }

    public void Reseat(BuildProcess buildProcess)
    {
        buildProcess.Started -= Reseat;
        _buildProcess = null;
    }
}