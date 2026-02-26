using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolMineral : ObjectPool<Mineral>
{
    [SerializeField] private List<MineralConfig> _configs  = new List<MineralConfig>();

    private Dictionary<MineralType, MineralConfig> _configsDictionary = new Dictionary<MineralType, MineralConfig>();

    public override void Initialize()
    {
        base.Initialize();

        for (int i = 0; i < _configs.Count; i++)
        {
            MineralType type = _configs[i].Type;
            _configsDictionary[type] = _configs[i];
        }
    }

    public Mineral GetMineral(MineralType type)
    {
        Mineral mineral = GetObject();

        if (mineral.Config != null && mineral.Config.Type == type)
            return mineral;

        mineral.SetConfig(_configsDictionary.GetValueOrDefault(type));

        return mineral;
    }
}