using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private float _useRadius;
    [SerializeField] private int _damage;
    
    [SerializeField] private Transform _overlapPoint;

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

        Debug.Log(brockables.Count);
        
        foreach (var brockable in brockables) 
            brockable.ApplyDamage(_damage);
    }
}
