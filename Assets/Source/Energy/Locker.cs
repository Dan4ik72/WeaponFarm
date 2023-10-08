using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Locker : MonoBehaviour, IQuestItem
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private ResourceType _currency;
    [SerializeField] private int _price;

    [SerializeField] private Collider _lockedObject;
    
    public event Action UnlockFaild; 
    public event Action UnlockSuccess;
    
    public event Action QuestCompleted;

    private void Awake() => Init();
    
    private void Init()
    {
        _lockedObject.enabled = !_isLocked;
        gameObject.SetActive(_isLocked);
    }
    
    public void TryUnlock(Inventory playerInventory)
    {
        if (playerInventory.TryGet(_currency, _price))
        {
            _isLocked = false;
            _lockedObject.enabled = true;
            gameObject.GetComponent<Collider>().enabled = false;
            UnlockSuccess?.Invoke();
            QuestCompleted?.Invoke();
            return;
        }

        UnlockFaild?.Invoke();
    }

}
