using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GrowingWeaponHandler : MonoBehaviour, IInteraction
{
    [SerializeField] private float _growIterationTime;
    [SerializeField] private GrowingWeaponFactory _growingWeaponFactory;
    private WeaponPlant _weaponPlant;
    private string _description = "Посадите оружие";
    
    public bool IsGrowing { get; private set; }

    public string InteractionDescription => _description;

    public event Action Grown;

    [ContextMenu("GrowTest Sword")]
    public void GrowTest()
    {
        Plant(SeedType.Sword);
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
            GrowAsync();
    }

    private async Task GrowAsync()
    {
        IsGrowing = true;
        _description = "Оружие растёт";
        _weaponPlant.Init();

        for (int i = 0; i < _weaponPlant.IterationsCount; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(_growIterationTime));

            _weaponPlant.IteratePlant();
        }

        Grown?.Invoke();
        _description = "Оружие выросло";
        IsGrowing = false;
    }

    public WeaponPlant WeaponGiveAway()
    {
        WeaponPlant toGiveAway = _weaponPlant;
        _weaponPlant = null;
        toGiveAway.transform.parent = null;
        return toGiveAway;
    }
}

public enum SeedType
{
    Null,
    Sword
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
