using System;
using System.Collections.Generic;
using UnityEngine;

public class BotHubFactory : BuildFactory
{
    [SerializeField] private BotHub _botHubPrefab;
    [SerializeField] private MineralRegistry _mineralRegistry;
    [SerializeField] private CollectorBotFactory _collectorBotSpawner;
    [SerializeField] private PriceList _priceList;
    [SerializeField] private CoroutineRunner _coroutineRunner;

    [SerializeField] private CellRegister _cellRegister;

    public event Action<BotHub> Created;

    public override Building Create(Vector3 position, List<Vector2Int> gridPosition)
    {
        BotHub botHub = Instantiate(_botHubPrefab, position, Quaternion.identity);
        botHub.gameObject.SetActive(false);

        botHub.Initialize(_mineralRegistry, _collectorBotSpawner, _coroutineRunner, _priceList);
        botHub.gameObject.SetActive(true);

        botHub.SetGridArea(gridPosition);
        _cellRegister.OccupyArea(botHub);

        Created?.Invoke(botHub);

        return botHub;
    }
}