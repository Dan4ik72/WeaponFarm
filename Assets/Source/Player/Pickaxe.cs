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

    public int Damage => _damage;
}