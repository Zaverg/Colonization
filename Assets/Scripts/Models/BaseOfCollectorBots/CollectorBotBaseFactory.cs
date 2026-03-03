using UnityEngine;

public class CollectorBotBaseFactory : MonoBehaviour
{
    [SerializeField] private CollectorBotBase _base;
    [SerializeField] private CollectorBotBaseConfig _config;

    private CollectorBaseService _collectorBaseService;
    private BaseMenuService _baseMenuService;

    public void Initialize(CollectorBaseService service, BaseMenuService baseMenuService)
    {
        _collectorBaseService = service;
        _baseMenuService = baseMenuService;
    }

    public CollectorBotBase Create(Vector3 position)
    {
        CollectorBotBase collectorBotBase = Instantiate(_base, position, Quaternion.identity);
        collectorBotBase.Initialize(_collectorBaseService, _baseMenuService);

        return collectorBotBase;
    }
}
