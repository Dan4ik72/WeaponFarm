using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private TMP_Text _ironCountText;
    [SerializeField] private TMP_Text _woodCountText;
    [SerializeField] private TMP_Text _weaponText;
    [SerializeField] private TMP_Text _seedText;
    
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
    
    public void UpdateInventory(int ironCount, int woodCount)
    {
        _ironCountText.text = ironCount.ToString();
        _woodCountText.text = woodCount.ToString();
    }

    public void SetActiveSeedText(bool isActive, string text = "")
    {
        _seedText.gameObject.SetActive(isActive);
        _seedText.text = text;
    }

    public void SetActiveWeaponText(bool isActive, string text = "")
    {
        _weaponText.gameObject.SetActive(isActive);
        _seedText.text = text;
    }
}
