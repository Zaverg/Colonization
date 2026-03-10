using System;
using UnityEngine;

public class CollectorBotBaseFactory : MonoBehaviour, IFactory
{
    [SerializeField] private CollectorBotBase _base;
    [SerializeField] private CollectorBotBaseConfig _config;

    private CollectorBaseService _collectorBaseService;

    public event Action<ICollectorBase> Created;

    public void Initialize(CollectorBaseService service)
    {
        _collectorBaseService = service;
    }

    public IBuildable Create(Vector3 position, bool isVisible)
    {
        CollectorBotBase collectorBotBase = Instantiate(_base, position, Quaternion.identity);
        collectorBotBase.gameObject.SetActive(false);

        collectorBotBase.Click += _collectorBaseService.BaseMenu.Show;

        collectorBotBase.Initialize(_collectorBaseService);

        collectorBotBase.gameObject.SetActive(isVisible);

        Created?.Invoke(collectorBotBase);

        return collectorBotBase;
    }
}