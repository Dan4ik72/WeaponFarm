﻿using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionCatcher : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;
    [SerializeField] private int _layer;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerEnergy _playerEnergy;
    
    private MeshRenderer _meshRenderer;
    private RaycastHit _ray;
    
    public event Action<IInteraction> InteractionEntered;
    public event Action<IInteractionWithRequirements> InteractionWithRequirementsEntered;
    public event Action<IInteraction> InteractionExited;

    private IInteraction _currentInteraction;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void Init()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void Update()
    {
        if (BoxcastForward())
        {
            if (_ray.collider.TryGetComponent(out ICollectable collectable))
            {
                _inventory.Collect((Resource)collectable);
            }
            
            if (_ray.collider.TryGetComponent(out Locker l))
            {
                InteractionWithRequirementsEntered?.Invoke(l);
                _currentInteraction = l;
            }
            
            if (_ray.collider.TryGetComponent(out Upgrade u))
            {
                InteractionWithRequirementsEntered?.Invoke(u);
                _currentInteraction = u;
            }

            if (_ray.collider.TryGetComponent(out EnergyGenerator e))
            {
                Debug.Log("energy generator");
                
                InteractionEntered?.Invoke(e);
                _currentInteraction = e;
            }
            
            if (Input.GetMouseButtonDown(0) && _currentInteraction != null)
            {
                if (_currentInteraction.GetType() == typeof(EnergyGenerator))
                {
                    var generator = (EnergyGenerator)_currentInteraction;
                    
                    _playerEnergy.ReceiveEnergy(generator.ReceiveEnergy());
                    return;
                }
                    
                _currentInteraction.Interact(_inventory);
            }
            
            return;
        }

        InteractionExited?.Invoke(_currentInteraction);
    }

    private bool BoxcastForward() =>
        Physics.BoxCast(_meshRenderer.bounds.center, transform.localScale, transform.forward,
            out _ray, transform.rotation, _raycastDistance, 1 << _layer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * _ray.distance);
        Gizmos.DrawWireCube(transform.position + transform.forward * _ray.distance, transform.localScale);
    }
}