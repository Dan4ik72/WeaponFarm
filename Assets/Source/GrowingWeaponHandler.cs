using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GrowingWeaponHandler : MonoBehaviour, IInteraction, IQuestItem
{
    //[SerializeField] private float _growIterationTime;
    [SerializeField] private GrowingWeaponFactory _growingWeaponFactory;
    [SerializeField] private int _energyCost;
    private WeaponPlant _weaponPlant;
    private string _description = "Plant weapon";

    private GardenBedState _currentState = GardenBedState.ReadyToPlant;

    public GardenBedState CurrentState => _currentState;

    public int EnergyCost => _energyCost;
    
    public bool IsGrowing { get; private set; }

    public string InteractionDescription => _description;

    public event Action Grown;

    private void Awake()
    {
        _description = "Plant weapon" + "\n" + _energyCost + " energy and 1 seed";
    }
    
    [ContextMenu("GrowTest Sword")]
    public void GrowTest()
    {
        Plant(SeedType.Gunana);
    }

    public void Interact(Inventory inventory)
    {
        if (inventory.TryGive(out SeedType seed) == false)
            return;

        Plant(seed);
    }

    public void Plant(SeedType seed)
    {
        if (_weaponPlant != null)
            return;
        
        _weaponPlant = _growingWeaponFactory.Create(seed);

        if (_weaponPlant != null)
        {
            GrowAsync();
            _currentState = GardenBedState.Growing;
        }
    }

    private async Task GrowAsync()
    {
        IsGrowing = true;
        _description = "Weapon is growing";
        _weaponPlant.Init();

        for (int i = 0; i < _weaponPlant.IterationsCount; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(_weaponPlant.GrowIterationTime));

            _weaponPlant.IteratePlant();
        }

        Grown?.Invoke();
        
        _description = "Weapon is ready";
        IsGrowing = false;
        _currentState = GardenBedState.WeaponGrown;
    }

    public WeaponPlant WeaponGiveAway()
    {
        WeaponPlant toGiveAway = _weaponPlant;
        _weaponPlant = null;
        toGiveAway.transform.parent = null;
        _currentState = GardenBedState.ReadyToPlant;
        _description = "Plant weapon" + "\n" + _energyCost + " energy and 1 seed";
        QuestCompleted?.Invoke();
        return toGiveAway;
    }

    public event Action QuestCompleted;
}

public enum GardenBedState
{
    ReadyToPlant,
    Growing,
    WeaponGrown
}

public enum SeedType
{
    Null,
    Gunana,
    Melomb,
    Pomegrenade
}

[System.Serializable]
public class GrowingWeaponFactory
{
    [SerializeField] private WeaponPlant[] _weaponPlantsPrefabs;
    [SerializeField] private Transform _growPoint;

    public WeaponPlant Create(SeedType seed)
    {
        foreach (var plant in _weaponPlantsPrefabs)
            if (plant.SeedType == seed)
                return CreateInternal(plant);

        return null;
    }

    private WeaponPlant CreateInternal(WeaponPlant prefab)
    {
        return GameObject.Instantiate(prefab, _growPoint.position, prefab.transform.rotation, _growPoint);
    }
}
