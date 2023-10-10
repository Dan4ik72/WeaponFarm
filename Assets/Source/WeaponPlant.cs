using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlant : MonoBehaviour
{
    [SerializeField] private SeedType _seedType;
    [SerializeField][Range(0, 100)] private float[] _iterations;
    [SerializeField] private float _growIterationTime;

    private int _currentIteration;
    public int IterationsCount => _iterations.Length;
    public float GrowIterationTime => _growIterationTime;

    public SeedType SeedType => _seedType;

    public void Init()
    {
        _currentIteration = 0;
        transform.localScale = Vector3.one * _iterations[_currentIteration] * 0.01f;
    }

    public void IteratePlant()
    {
        _currentIteration = Mathf.Min(_currentIteration + 1, _iterations.Length - 1);
        transform.localScale = Vector3.one * _iterations[_currentIteration] * 0.01f;
    }
}
