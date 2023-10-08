using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ResourceType, int> _resources = new();

    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        _resources.Add(ResourceType.Iron, 4);
        _resources.Add(ResourceType.Copper, 0);
        _resources.Add(ResourceType.Silver, 0);
        _resources.Add(ResourceType.Gold, 0);
    }
    
    public void Collect(Resource resource)
    {
        _resources[resource.Type] += 1;
        
        Destroy(resource.gameObject);
    }

    public bool TryGet(ResourceType type, int count)
    {
        if (_resources[type] < count)
            return false;

        _resources[type] -= count;
        
        return true;
    }
}
