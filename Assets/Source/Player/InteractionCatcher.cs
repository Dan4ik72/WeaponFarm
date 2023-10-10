using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionCatcher : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;
    [SerializeField] private int _layer;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerEnergy _playerEnergy;
    [SerializeField] private Pickaxe _pickaxe;
    [SerializeField] private Transform _raycastStart;
    
    private MeshRenderer _meshRenderer;
    private RaycastHit _hit;
    
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
            if (_hit.collider.TryGetComponent(out ICollectable collectable))
            {
                _inventory.Collect((Resource)collectable);
            }
            
            if (_hit.collider.TryGetComponent(out Locker l))
            {
                InteractionWithRequirementsEntered?.Invoke(l);
                _currentInteraction = l;
            }
            
            if (_hit.collider.TryGetComponent(out Upgrade u))
            {
                InteractionWithRequirementsEntered?.Invoke(u);
                _currentInteraction = u;
            }

            if (_hit.collider.TryGetComponent(out EnergyGenerator e))
            {
                InteractionEntered?.Invoke(e);
                _currentInteraction = e;
            }

            if (_hit.collider.TryGetComponent(out IBrockable brockable))
            {
                InteractionEntered?.Invoke(brockable);
                _currentInteraction = brockable;
            }

            if (_hit.collider.TryGetComponent(out GrowingWeaponHandler gardenBed))
            {
                InteractionEntered?.Invoke(gardenBed);
                _currentInteraction = gardenBed;
            }
            
            if(_hit.collider.TryGetComponent(out WeaponTraider weaponTraider))
            {
                InteractionEntered?.Invoke(weaponTraider);
                _currentInteraction = weaponTraider;
            }

            if (_hit.collider.TryGetComponent(out SeedHolder seedHolder))
            {
                InteractionEntered?.Invoke(seedHolder);
                _currentInteraction = seedHolder;
            }

            if (_hit.collider.TryGetComponent(out SeedLaboratory laboratory))
            {
                InteractionWithRequirementsEntered?.Invoke(laboratory);
                _currentInteraction = laboratory;
            }
            
            if (Input.GetMouseButtonDown(0) && _currentInteraction != null)
            {
                if (_currentInteraction.GetType() == typeof(EnergyGenerator))
                {
                    var generator = (EnergyGenerator)_currentInteraction;
                    
                    _playerEnergy.ReceiveEnergy(generator.ReceiveEnergy());
                    return;
                }

                if (_currentInteraction is IBrockable brock)
                {
                    if(_playerEnergy.TrySpendEnergy(brock.GetEnergyEffect()) == false)
                        return;
                    
                    brock.ApplyDamage(_pickaxe.Damage);
                    return;
                }

                if (_currentInteraction is GrowingWeaponHandler bed)
                {
                    switch (bed.CurrentState)
                    {
                        case GardenBedState.ReadyToPlant:
                            if(_inventory.CurrentSeed == SeedType.Null)
                                return;
                            if (_playerEnergy.TrySpendEnergy(bed.EnergyCost) == false)
                                return;
                            bed.Interact(_inventory);
                            break;
                        
                        case GardenBedState.Growing:
                            break;
                        
                        case GardenBedState.WeaponGrown:
                            var weapon = bed.WeaponGiveAway();
                            _inventory.CollectWeapon(weapon.SeedType);
                            Destroy(weapon.gameObject);
                            break;
                    }
                    
                    return;
                }

                if (_currentInteraction is SeedHolder sh)
                {
                    if(sh.IsFilled == false)
                        return;
                    
                    if(sh.IsAvailable == false)
                        return;
                    
                    if (_playerEnergy.TrySpendEnergy(sh.EnergyRequired) == false)
                        return;
                    
                    sh.Interact(_inventory);
                    
                    return;
                }
                
                _currentInteraction.Interact(_inventory);
            }
            
            return;
        }

        InteractionExited?.Invoke(_currentInteraction);
    }

    private bool BoxcastForward()
    {
        //return Physics.BoxCast(transform.position, transform.localScale, transform.forward,
        //    out _hit, transform.rotation, _raycastDistance, 1 << _layer);
        Ray ray = new Ray(_raycastStart.position, transform.forward);
        return Physics.Raycast(ray, out _hit, _raycastDistance, 1 << _layer);
    }

    private void OnDrawGizmos()
    {
        if (BoxcastForward() == true)
        {
            Gizmos.DrawRay(_raycastStart.position, transform.forward * _hit.distance);
            //Gizmos.DrawWireCube(transform.position + transform.forward * _hit.distance, transform.localScale);
        }
        else
        {
            Gizmos.DrawRay(_raycastStart.position, transform.forward * _raycastDistance);
            //Gizmos.DrawWireCube(transform.position + transform.forward * _raycastDistance, transform.localScale);
        }
    }
}