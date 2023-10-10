using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedLaboratory : MonoBehaviour,IInteractionWithRequirements, IQuestItem
{
    [SerializeField] private List<Transform> _potsSpawnPoints;
    [SerializeField] private List<SeedHolder> _seedHolders;
    [SerializeField] private List<SeedLaboratoryRequirements> _requirements;

    private int _index = 0;

    public string InteractionDescription { get; private set; }

    private void Awake()
    {
        InteractionDescription = "Open new seed";
    }
    
    public void Interact(Inventory inventory)
    {
        if(_index == _requirements.Count)
            return;

        if (inventory.TryGive(_requirements[_index].ResourceType, _requirements[_index].Count) == false)
        {
            InteractionDescription = "You dont have enough resources";
            return;
        }

        Instantiate(_seedHolders[_index], _potsSpawnPoints[_index].position, _seedHolders[_index].transform.rotation, _potsSpawnPoints[_index]);

        InteractionDescription = "Open new seed";

        QuestCompleted?.Invoke();
        
        _index++;
    }

    public (ResourceType, int) GetRequirements()
    {
        if (_index == _requirements.Count)
        {
            InteractionDescription = "";
            return default;
        }
        
        return (_requirements[_index].ResourceType, _requirements[_index].Count);
    }

    public event Action QuestCompleted;
}

[System.Serializable]
public class SeedLaboratoryRequirements
{
    public ResourceType ResourceType;
    public int Count;
}
