using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyView : MonoBehaviour
{
    [SerializeField] private Slider _energySlider;
    [SerializeField] private TMP_Text _sliderText;
    [SerializeField] private PlayerEnergy _playerEnergy;

    private void OnEnable()
    {
        _playerEnergy.OnEnergyChange += UpdateView;
    }

    private void OnDisable()
    {
        _playerEnergy.OnEnergyChange -= UpdateView;
    }

    private void UpdateView(int energy, int maxEnergy)
    {
        _energySlider.maxValue = maxEnergy;
        _energySlider.value = Mathf.Min(energy, maxEnergy);
        _sliderText.text = energy + "/" + maxEnergy;
    }
}
