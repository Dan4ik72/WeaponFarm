using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private SeedType _currentSeed;
    private Dictionary<ResourceType, int> _resources = new();

    private void Awake()
    {
        Init();
    }
    
    public void Collect(SeedType seed)
    {
        _currentSeed = seed;
    }

    private void Init()
    {
        _resources.Add(ResourceType.Iron, 4);
        _resources.Add(ResourceType.Copper, 4);
        _resources.Add(ResourceType.Silver, 4);
        _resources.Add(ResourceType.Gold, 4);
    }
    
    public void Collect(Resource resource)
    {
        _resources[resource.Type] += 1;
        
        Destroy(resource.gameObject);
    }

    public bool TryGive(out SeedType seed)
    {
        seed = _currentSeed;

        if (_currentSeed == SeedType.Null)
            return false;

        _currentSeed = SeedType.Null;
        return true;
    }

    public bool TryGive(ResourceType type, int count)
    {
        if (_resources[type] < count)
            return false;

        _resources[type] -= count;
        
        return true;
    }
}
