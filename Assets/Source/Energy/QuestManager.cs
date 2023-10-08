using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _questsSequence;
    [Space]
    [SerializeField] private Quest _first;
    
    private Quest _currentQuest;

    private int _currentQuestIndex;

    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        _currentQuestIndex = 0;
        _currentQuest = _first;
        _currentQuest.Enable();
        _currentQuest.QuestEnded += ChangeQuest;
    }

    private void ChangeQuest()
    {
        _currentQuestIndex++;
        _currentQuest.QuestEnded -= ChangeQuest;
        _currentQuest.OnQuestEnded();
        
        if (_questsSequence.Count - 1 < _currentQuestIndex)
            return;
     
        var newQuest = _questsSequence[_currentQuestIndex];
        newQuest.Enable();
        newQuest.QuestEnded += ChangeQuest;
        _currentQuest = newQuest;
    }
}

[Serializable]
public class Quest
{
    [SerializeField] private GameObject _questItem;

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
