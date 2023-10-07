using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ResourceType> _collectedResources = new();

    public IReadOnlyList<ResourceType> CollectedResources => _collectedResources;

    public void Collect(Resource resource)
    {
        _collectedResources.Add(resource.Type);
        
        Debug.Log("Collected");
        Destroy(resource.gameObject);
    }
}
