using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _questsSequence;

    [SerializeField] private TMP_Text _text;
    
    private Quest _currentQuest;

    public Quest CurrentQuest => _currentQuest;
    
    private int _currentQuestIndex;

    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        _currentQuestIndex = 0;
        _currentQuest = _questsSequence.First();
        _currentQuest.Enable();
        _text.text = _currentQuest.QuestDescription;
        _currentQuest.QuestEnded += ChangeQuest;
    }

    private void ChangeQuest()
    {
        Debug.Log("Change");
        
        _currentQuestIndex++;
        _currentQuest.QuestEnded -= ChangeQuest;
        _currentQuest.OnQuestEnded();
        _text.gameObject.SetActive(false);
        
        if (_questsSequence.Count - 1 < _currentQuestIndex)
            return;

        _text.gameObject.SetActive(true);
        var newQuest = _questsSequence[_currentQuestIndex];
        newQuest.Enable();
        newQuest.QuestEnded += ChangeQuest;
        _currentQuest = newQuest;
        _text.text = _currentQuest.QuestDescription;
        Debug.Log($"_currentQuest.QuestDescription {_currentQuest.QuestDescription}");
    }
}

[Serializable]
public class Quest
{
    [SerializeField] private GameObject _questItem;
    [SerializeField] private string _questDescription;

    public string QuestDescription => _questDescription;
    
    public event Action QuestEnded;
    
    public void Enable()
    {
        var questable = _questItem.GetComponent<IQuestItem>();
     
        questable.QuestCompleted += OnQuestEnded;
    }

    public void OnQuestEnded()
    {
        QuestEnded?.Invoke();
    }
}

public interface IQuestItem
{
    public event Action QuestCompleted;
}
