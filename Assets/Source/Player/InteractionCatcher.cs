using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionCatcher : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;
    [SerializeField] private int _layer;
    [SerializeField] private Inventory _inventory;
    
    private MeshRenderer _meshRenderer;
    private RaycastHit _ray;

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

            if (Input.GetMouseButtonDown(0))
            {
                if (_ray.collider.TryGetComponent(out Locker locked))
                {
                    locked.TryUnlock(_inventory);
                }    
            }
        }
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
