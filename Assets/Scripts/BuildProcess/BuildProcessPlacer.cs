using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildProcessPlacer : MonoBehaviour
{
    [SerializeField] private CellRegister _cellRegister;
    [SerializeField] private BuildProcessFactory _buildProcessFactory;
    [SerializeField] private BotHubMenu _botHubMenu;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Grid _grid;

    private Flag _flag;
    private BuildProcess _buildProcess;

    private Vector2Int _lastGridPosition;
    private List<Vector2Int> _gridPositions = new List<Vector2Int>();

    public void Update()
    {
        if (_buildProcess == null)
            return;

        Vector2Int currentGridPosition = _grid.ConvertWorldToGridPosition(_buildProcess.transform.position);

        if (_lastGridPosition != currentGridPosition)
        {
            List<Vector3> area = _buildProcess.CalculateArea();
            _gridPositions = _cellRegister.TryGetOccupyArea(new List<Vector3>(area));

            _lastGridPosition = currentGridPosition;
        }
    }

    public void CompletePlacement(Transform surface)
    {
        _inputReader.OnClick -= CompletePlacement;
       
        if (surface.TryGetComponent<Map>(out _) == false || _gridPositions.Count == 0)
        {
            _buildProcess.Release();
            _buildProcess = null;

            return;
        }

        _cellRegister.ReserveArea(_gridPositions);

        _buildProcess.Install(_gridPositions);

        _flag.Install(_buildProcess.transform.position);
        _flag.SetBuildProcess(_buildProcess);

        _buildProcess.gameObject.SetActive(false);
        _buildProcess = null;
        _flag = null;
    }

    public void StartPlacement(BuildType type)
    {
        _flag = _botHubMenu.CurrentBase.Flag;

        if (_flag.gameObject.activeSelf)
        {
            _cellRegister.FreeCells(_flag.BuildProcess.OccupyArea.ToList());
            _flag.Deactivate();
        }

        if (_buildProcess != null)
        {
            _buildProcess.Release();
        }

        _buildProcess = _buildProcessFactory.Create(type);

        _inputReader.OnClick -= CompletePlacement;
        _inputReader.OnClick += CompletePlacement;
    }
}