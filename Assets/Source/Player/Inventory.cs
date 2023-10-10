using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryView _inventoryView;
    
    private SeedType _currentSeed;
    private SeedType _weapon;
    
    private Dictionary<ResourceType, int> _resources = new();

    public InventoryView InventoryView => _inventoryView;
    
    private void Awake()
    {
        Init();
        _inventoryView.SetActiveSeedText(false);
        _inventoryView.SetActiveWeaponText(false);
    }

    private void Update()
    {
        _inventoryView.UpdateInventory(_resources[ResourceType.Iron], _resources[ResourceType.Wood]);
    }
    
    public void Collect(SeedType seed)
    {
        _currentSeed = seed;
        _inventoryView.ShowText("You got " + seed + " seed");
        _inventoryView.SetActiveSeedText(true ,"1 " + seed + " seed");
    }

    public void CollectWeapon(SeedType weapon)
    {
        _weapon = weapon;
        _inventoryView.ShowText("You got " + weapon + " weapon");
        _inventoryView.SetActiveWeaponText(true);
    }

    private void Init()
    {
        _resources.Add(ResourceType.Iron, 4);
        _resources.Add(ResourceType.Copper, 4);
        _resources.Add(ResourceType.Silver, 4);
        _resources.Add(ResourceType.Gold, 4);
        _resources.Add(ResourceType.Wood, 4);
        _currentSeed = SeedType.Sword;
        _inventoryView.ShowText("You got basic resources");
    }
    
    public void Collect(Resource resource)
    {
        _resources[resource.Type] += 1;

        _inventoryView.ShowText("You got " + resource.Type);
        
        Destroy(resource.gameObject);
    }

    public bool TryGiveWeapon(out SeedType weapon)
    {
        weapon = _weapon;

        if (weapon == SeedType.Null)
            return false;

        _inventoryView.ShowText(weapon + " weapon sold");
        _inventoryView.SetActiveWeaponText(false);
        _weapon = SeedType.Null;
        return true;
    }
    
    public bool TryGive(out SeedType seed)
    {
        seed = _currentSeed;

        if (_currentSeed == SeedType.Null)
            return false;

        _inventoryView.ShowText(seed + " seed spent");
        _inventoryView.SetActiveSeedText(false);
        
        _currentSeed = SeedType.Null;
        return true;
    }

    public bool TryGive(ResourceType type, int count)
    {
        if (_resources[type] < count)
            return false;

        _resources[type] -= count;

        _inventoryView.ShowText("You spent " + count + " " + type + "s");
        
        return true;
    }
}
