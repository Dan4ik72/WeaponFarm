using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour, IInteraction, IQuestItem
{
    [SerializeField] private int _maxEnergy;
    [SerializeField] private float _timeStepsToFillEnergy;
    [SerializeField] private int _energyFillingStep;
    
    [SerializeField] private int _currentEnergy = 0;
    
    private void Awake()
    {
        StartCoroutine(FillEnergy());
    }

    public int ReceiveEnergy()
    {
        var receiving = _currentEnergy;
        _currentEnergy = 0;
        QuestCompleted?.Invoke();
        return receiving;
    }

    private IEnumerator FillEnergy()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_timeStepsToFillEnergy);
            
            _currentEnergy += _energyFillingStep;

            if (_currentEnergy > _maxEnergy)
                _currentEnergy = _maxEnergy;
        }
    }

    public string InteractionDescription => "Get Energy " + _currentEnergy;

    public void Interact(Inventory inventory)
    {
        throw new NotImplementedException();
    }

    public event Action QuestCompleted;
}
