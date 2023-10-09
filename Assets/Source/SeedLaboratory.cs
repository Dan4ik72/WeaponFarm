using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedLaboratory : MonoBehaviour,IInteractionWithRequirements
{
    [SerializeField] private List<Transform> _potsSpawnPoints;
    [SerializeField] private List<SeedHolder> _seedHolders;
    [SerializeField] private List<SeedLaboratoryRequirements> _requirements;

    private int _index = 0;
    
    public string InteractionDescription { get; }
    
    public void Interact(Inventory inventory)
    {
        if(inventory.TryGive(_requirements[_index].ResourceType, _requirements[_index].Count) == false)
            return;

        Instantiate(_seedHolders[_index], _potsSpawnPoints[_index]);

        _index++;
    }

    public (ResourceType, int) GetRequirements()
    {
        return (_requirements[_index].ResourceType, _requirements[_index].Count);
    }
}

[System.Serializable]
public class SeedLaboratoryRequirements
{
    public ResourceType ResourceType;
    public int Count;
}
