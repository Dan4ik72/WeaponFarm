using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject _current;
    
    [SerializeField] private List<GameObject> _buildings;

    private int _upgradeIndex = 0;
    
    private void Update()
    {
        if(_buildings.Count - 1 < _upgradeIndex)
            return;
        
        _current.gameObject.SetActive(false);
        _current = _buildings[_upgradeIndex];
        _current.gameObject.SetActive(true);
    }
}
