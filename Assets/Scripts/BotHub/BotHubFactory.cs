using System;
using System.Collections.Generic;
using UnityEngine;

public class BotHubFactory : MonoBehaviour
{
    [SerializeField] private BotHub _botHubPrefab;
    [SerializeField] private MineralRegistry _mineralRegistry;
    [SerializeField] private CollectorBotFactory _collectorBotSpawner;
    [SerializeField] private PriceList _priceList;
    [SerializeField] private CoroutineRunner _coroutineRunner;

    [SerializeField] private CellRegister _cellRegister;
    [SerializeField] private BuildProcessPlacer _buildProccesPlacer;

    public event Action<BotHub> Created;

    public BotHub Create(Vector3 position, List<Vector2Int> gridPosition)
    {
        BotHub botHub = Instantiate(_botHubPrefab, position, Quaternion.identity);

        botHub.Initialize(_mineralRegistry, _collectorBotSpawner, _coroutineRunner, _priceList);

        botHub.SetGridArea(gridPosition);
        _cellRegister.OccupyArea(botHub);

        BotHubMenu botHubMenu = botHub.GetComponentInChildren<BotHubMenu>();

        if (botHubMenu != null)
            botHubMenu.Initialize(_buildProccesPlacer);

        Created?.Invoke(botHub);

        return botHub;
    }
}
