using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTraider : MonoBehaviour, IInteraction
{
    public string InteractionDescription => "Trade your weapon";
    
    public void Interact(Inventory inventory)
    {
        if(inventory.TryGiveWeapon(out SeedType weapon) == false)
            return;
        
        inventory.Collect(SeedType.Sword);
    }
}
