using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private float _useRadius;
    [SerializeField] private int _damage;
    
    [SerializeField] private Transform _overlapPoint;

    [SerializeField] private PlayerEnergy _playerEnergy;
    
    [SerializeField] private int _layer;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Use();
        }
    }

    private void Use()
    {
        var brockables = Physics.OverlapSphere(_overlapPoint.position, _useRadius, 1 << _layer).
            ToList().Select(collider => collider.GetComponent<IBrockable>()).ToList();
        
        if(brockables == null || brockables.Count == 0)
            return;
        
        var first = brockables.First();
        
        if(_playerEnergy.TrySpendEnergy(first.GetEnergyEffect()) == false)
            return;

        first.ApplyDamage(_damage);
    }
}