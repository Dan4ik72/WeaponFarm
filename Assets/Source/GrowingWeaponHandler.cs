using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GrowingWeaponHandler : MonoBehaviour
{
    [SerializeField] private float _growIterationTime;
    [SerializeField] private GrowingWeaponFactory _growingWeaponFactory;
    private WeaponPlant _weaponPlant;
    
    public bool IsGrowing { get; private set; }

    public event Action Grown;

    [ContextMenu("GrowTest Sword")]
    public void GrowTest()
    {
        Plant(SeedType.Sword);
    }

    public void Plant(SeedType seed)
    {
        _weaponPlant = _growingWeaponFactory.Create(seed);

        if (_weaponPlant != null)
            GrowAsync();
    }

    private async Task GrowAsync()
    {
        _weaponPlant.Init();

        for (int i = 0; i < _weaponPlant.IterationsCount; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(_growIterationTime));

            _weaponPlant.IteratePlant();
        }

        Grown?.Invoke();
    }
}

public enum SeedType
{
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
