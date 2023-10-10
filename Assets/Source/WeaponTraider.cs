using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTraider : MonoBehaviour, IInteraction
{
    public string InteractionDescription => "Trade your weapon";

    public void Interact(Inventory inventory)
    {
        if (inventory.TryGiveWeapon(out SeedType weapon) == false)
        {
            inventory.InventoryView.ShowText("You dont have a weapon to sold");
            return;
        }

        inventory.Collect(SeedType.Sword);
    }
}
