using System;
using UnityEngine;

public class ResourcesDeposite : MonoBehaviour, IBrockable
{
    [SerializeField] private Resource _resourcesPrefab;
    [SerializeField] private int _resourcesCount;
    [Space]
    [SerializeField] private int _durability;
    
    private int _currentDurability;

    public void Init() => _currentDurability = _durability;

    private void Awake()
    {
        //OnDie();
    }
    
    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _currentDurability = Mathf.Clamp(_currentDurability - damage, 0, _currentDurability);
        
        if(_currentDurability == 0)
            OnDie();
    }

    private void OnDie()
    {
        transform.GetComponent<Collider>().enabled = false;
        
        for (int i = 0; i < _resourcesCount; i++)
        {
            var rigidbody = Instantiate(_resourcesPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            
            rigidbody.AddForce(Vector3.up, ForceMode.Force);
        }
        
        Destroy(gameObject);
    }

    public int GetCurrentDurability() => _currentDurability;

}