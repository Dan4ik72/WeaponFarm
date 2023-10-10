using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTraider : MonoBehaviour, IInteraction, IQuestItem
{
    public string InteractionDescription => "Trade your weapon";

    public void Interact(Inventory inventory)
    {
        if (inventory.TryGiveWeapon(out SeedType weapon) == false)
        {
            inventory.InventoryView.ShowText("You dont have a weapon to sold");
            return;
        }

        QuestCompleted?.Invoke();
        
        switch (weapon)
        {
            case SeedType.Gunana:
            inventory.CollectResourceByType(ResourceType.Wood, 2);
                return;
            
            case SeedType.Melomb:
                inventory.CollectResourceByType(ResourceType.Iron, 4);
                return;
            
            case SeedType.Pomegrenade:
                inventory.CollectResourceByType(ResourceType.Wood, 10);
                return;
        }
        
    }

    public event Action QuestCompleted;
}
