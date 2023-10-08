using System.Linq;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [Header("Overlap settings")]
    [SerializeField] private Transform _overlapPoint;
    [SerializeField] private float _useRadius;
    [SerializeField] private int _layer;
    [Space]
    [SerializeField] private int _energy;

    public int CurrentEnergy => _energy;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Overlap();
    }
    
    public bool TrySpendEnergy(int energy)
    {
        if(energy > CurrentEnergy)
            return false;

        _energy -= energy;
        return true;
    }
    
    private void Overlap()
    {
        var colliders = Physics.OverlapSphere(_overlapPoint.position, _useRadius, 1 << _layer).
            ToList().Select(collider => collider.GetComponent<EnergyGenerator>()).ToList();
        
        if(colliders.Count == 0)
            return;

        var generator = colliders.First();
        
        ReceiveEnergy(generator.ReceiveEnergy());
    }
    
    private void ReceiveEnergy(int energy) => _energy += energy;
}