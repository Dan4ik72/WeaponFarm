using System;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [Header("Overlap settings")]
    [SerializeField] private Transform _overlapPoint;
    [SerializeField] private float _useRadius;
    [SerializeField] private int _layer;
    [Space]
    [SerializeField] private int _energy, _maxEnergy;

    public Action<int, int> OnEnergyChange;
    public int CurrentEnergy => _energy;

    private void Start()
    {
        OnEnergyChange?.Invoke(_energy, _maxEnergy);
    }
    public bool TrySpendEnergy(int energy)
    {
        if(energy > CurrentEnergy)
            return false;

        _energy -= energy;
        OnEnergyChange?.Invoke(_energy, _maxEnergy);
        return true;
    }

    public void ReceiveEnergy(int energy)
    {
        _energy += energy;
        OnEnergyChange?.Invoke(_energy, _maxEnergy);
    }
}