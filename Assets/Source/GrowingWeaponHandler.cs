using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GrowingWeaponHandler : MonoBehaviour
{
    [SerializeField] private float _growIterationTime;

    private GrowingWeaponFactory _growingWeaponFactory;
    
    public bool IsGrowing { get; private set; }

    public event Action Grown;
    
    public void Plant(SeedType seed)
    {
         
    }

    private async Task GrowAsync()
    {
        _growingWeaponFactory.Create();

        for (int i = 0; i < 3; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(_growIterationTime));

            IteratePlant();
        }

        Grown?.Invoke();
    }

    private void IteratePlant()
    {
        
    }
}

public enum SeedType
{
    Sword
}

[System.Serializable]
public class GrowingWeaponFactory
{
    public void Create()
    {
        throw new System.NotImplementedException();
    }
}
