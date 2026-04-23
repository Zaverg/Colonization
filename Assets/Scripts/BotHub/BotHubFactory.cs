using System;
using UnityEngine;

public class BotHubFactory : Factory
{
    [SerializeField] private BotHub _botHubPrefab;
    [SerializeField] private CollectorBotBaseConfig _config;

    private BaseService _collectorBaseService;

    public event Action<IBotHub> Created;

    public void Initialize(BaseService service)
    {
        _collectorBaseService = service;
    }

    public override Building Create(Vector3 position, bool startActive)
    {
        BotHub collectorBotBase = Instantiate(_botHubPrefab, position, Quaternion.identity);
        collectorBotBase.gameObject.SetActive(false);

        collectorBotBase.Click += _collectorBaseService.BaseMenu.Show;

        collectorBotBase.Initialize(_collectorBaseService);

        collectorBotBase.gameObject.SetActive(startActive);

        Created?.Invoke(collectorBotBase);

        return collectorBotBase;
    }
}
