using System.Collections.Generic;
using UnityEngine;

public class MapInitializer : MonoBehaviour
{
    [SerializeField] private CellRegister _cellRegister;
    [SerializeField] private ObjectPoolMineral _mineralObjectPool;
    [SerializeField] private Map _map;
    [SerializeField] private Grid _grid;
    [SerializeField] private MineralRegistry _mineralRegistry;
    [SerializeField] private MineralSpawner _mineralSpawner;

    private GridCreator _gridCreator;

    public CellRegister CellRegister => _cellRegister;
    public Map Map => _map;

    public void Initialize(CoroutineRunner coroutineRunner)
    {
        _cellRegister.gameObject.SetActive(false);
        _mineralSpawner.gameObject.SetActive(false);

        _map.Initialize();
        _mineralObjectPool.Initialize();

        _gridCreator = new GridCreator();
        List<List<Cell>> grid = _gridCreator.Create(_map, _grid.CellSizeGrid);

        _grid.Initialize(grid);
        _cellRegister.Initialize();

        _mineralSpawner.Initialize(coroutineRunner);
    }
}
