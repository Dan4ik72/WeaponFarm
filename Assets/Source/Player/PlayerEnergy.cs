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
    
    public bool TrySpendEnergy(int energy)
    {
        if(energy > CurrentEnergy)
            return false;

        _energy -= energy;
        return true;
    }
    
    public void ReceiveEnergy(int energy) => _energy += energy;
}