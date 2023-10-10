using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionViewHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _interactionText;
    [SerializeField] private GameObject _interactionObject;
    [SerializeField] private InteractionCatcher _interactionCatcher;

    private IInteraction _current;
    
    private void Awake()
    {
        _interactionCatcher.InteractionEntered += ShowInteraction;
        _interactionCatcher.InteractionWithRequirementsEntered += ShowInteractionWithRequirements;
        _interactionCatcher.InteractionExited += OnInteractionEnded; 
    }
    
    private void ShowInteraction(IInteraction interaction)
    {
        _current = interaction;
        _interactionText.text = interaction.InteractionDescription;
        _interactionObject.SetActive(true);
    }

    private void ShowInteractionWithRequirements(IInteractionWithRequirements interaction)
    {
        _current = interaction;
        var requirements = interaction.GetRequirements();
        _interactionText.text = interaction.InteractionDescription + "\n" + requirements.Item1 + " " + requirements.Item2;
        _interactionObject.SetActive(true);
    }

    private void OnInteractionEnded(IInteraction interaction)
    {
        if(interaction != _current)
            return;

        //_interactionObject.SetActive(false);
    }
}

public interface IInteractionWithRequirements : IInteraction
{
    public (ResourceType, int) GetRequirements();
}

public interface IInteraction
{
    public string InteractionDescription { get; }

    public void Interact(Inventory inventory);
}
