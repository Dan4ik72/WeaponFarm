using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SeedHolder : MonoBehaviour, IInteraction
{
    [SerializeField] private SeedType _seedType;

    [SerializeField] private float _growTime;
    [SerializeField] private int _energyRequired;
    
    private bool _isFilled = true;

    public int EnergyRequired => _energyRequired;
    public int SeedIndex => (int)_seedType;
    public string InteractionDescription { get; private set; }
    
    public void Interact(Inventory inventory)
    {
        if(_isFilled == false)
            return;
        
        inventory.Collect(_seedType);
        _isFilled = false;
    }
}
