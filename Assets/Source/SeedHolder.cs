using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Build;
using UnityEngine;

public class SeedHolder : MonoBehaviour, IInteraction, IQuestItem
{
    [SerializeField] private SeedType _seedType;

    [SerializeField] private int _energyRequired;

    [SerializeField] private float _cooldown;
    
    private bool _isFilled = true;
    private bool _isAvailable = true;
    private string _description;


    public bool IsFilled => _isFilled;
    public bool IsAvailable => _isAvailable;
    public int EnergyRequired => _energyRequired;
    public int SeedIndex => (int)_seedType;
    public string InteractionDescription
    {
        get => _description;
        private set => _description = value;
    }

    private void Awake()
    {
        _description = $"Get {_seedType} seed.\n{_energyRequired} energy required";
    }
    
    public void Interact(Inventory inventory)
    {
        if(_isFilled == false)
            return;
     
        if(_isAvailable == false)
            return;
        
        inventory.Collect(_seedType);
        _isFilled = true;
        QuestCompleted?.Invoke();
        Cooldown();
    }

    private async Task Cooldown()
    {
        _isAvailable = false;
        
        _description = "Wait " + _cooldown + " sec for new seed";
            
        await Task.Delay(TimeSpan.FromSeconds(_cooldown));
        
        _description = $"Get {_seedType} seed.\n{_energyRequired} energy required";
        
        _isAvailable = true;
    }

    public event Action QuestCompleted;
}
