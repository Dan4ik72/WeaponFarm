using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour, IInteractionWithRequirements
{
    [SerializeField] private GameObject _current;
    
    [SerializeField] private List<GameObject> _buildings;
    [SerializeField] private ResourceType _currency;
    [SerializeField] private int _price;
    
    private int _upgradeIndex = 0;

    public string InteractionDescription => "Upgrade the building?";
    
    public void Interact(Inventory inventory)
    {
        InterateUpgrade(inventory);
    }

    public void InterateUpgrade(Inventory playerInventory)
    {
        Debug.Log("tryIterate");
        
        if(playerInventory.TryGive(_currency, _price) == false)
            return;
        
        Debug.Log("HasMoney");
        
        if(_buildings.Count - 1 < _upgradeIndex)
            return;
        
        _current.gameObject.SetActive(false);
        _current = _buildings[_upgradeIndex];
        _current.gameObject.SetActive(true);

        _upgradeIndex++;
    }

    public (ResourceType, int) GetRequirements() => (_currency, _price);
}
