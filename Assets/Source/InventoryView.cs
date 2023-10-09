using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    
    public async Task ShowText(string text)
    {
        gameObject.SetActive(true);
        _text.text = text;
        await Task.Delay(TimeSpan.FromSeconds(3));
        gameObject.SetActive(false);
    }

    public void UpdateInventory(List<string> resources)
    {
        
    }
}
