using System;
using UnityEngine;

public class CollectorBotBaseFactory : MonoBehaviour
{
    [SerializeField] private CollectorBotBase _base;
    [SerializeField] private CollectorBotBaseConfig _config;

    private CollectorBaseService _collectorBaseService;

    public event Action<ICollectorBase> Created;

    public void Initialize(CollectorBaseService service)
    {
        _collectorBaseService = service;
    }

    public CollectorBotBase Create(Vector3 position)
    {
        CollectorBotBase collectorBotBase = Instantiate(_base, position, Quaternion.identity);
        collectorBotBase.gameObject.SetActive(false);

        collectorBotBase.Click += _collectorBaseService.BaseMenu.Show;

        collectorBotBase.Initialize(_collectorBaseService);

        collectorBotBase.gameObject.SetActive(true);

        Created?.Invoke(collectorBotBase);

        return collectorBotBase;
    }
}
