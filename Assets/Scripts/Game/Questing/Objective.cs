using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Interaction;
using Game.Items;
using Game.Questing;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Objective
{
    public enum Goals
    {
        Interact,
        Kill,
        Talk,
        InitTalk,
        Use
    }

    [SerializeField] private string identifier;
    [SerializeField] private string description;
    
    [SerializeField] private List<Interactable> targets;
    private Dictionary<Interactable, bool> _targets;
    
    [Header("Use only for items")]
    [SerializeField] private string targetString;
    private Item itemTarget;

    [SerializeField] private Goals goal;

    [SerializeField] private UnityEvent eEvent;

    public Goals Goal => goal;
    public List<Interactable> Targets => targets;
    public string Description => description;
    public string Identifier => identifier;

    public Item ItemObjective => itemTarget;

    public Objective(string description, List<Interactable> targets, Goals goal)
    {
        this.description = description;
        this.goal = goal;
        this.targets = targets;
    }

    public void Init()
    {
        _targets = new Dictionary<Interactable, bool>();
        foreach (Interactable interactable in targets)
        {
            if (interactable != null)
            {
                _targets.Add(interactable, false);
            }
        }

        if (targetString.Length > 0)
        {
            itemTarget = ItemDatabase.Instance.GetItem(targetString);
        }
    }

    public void OnObjectiveActivated()
    {
        foreach (var target in targets)
        {
            target.HandleMarker(true);
        }

        if (eEvent != null)
        {
            eEvent.Invoke();
        }
    }
    
    public void OnObjectiveDeactivated()
    {
        foreach (var target in targets)
        {
            target.HandleMarker(false);
        }
    }

    public void CompleteTarget(Interactable target)
    {
        if (_targets.ContainsKey(target))
        {
            QuestManager.Instance.UpdateUI();
            _targets[target] = true;
            CheckForCompletion();
        }
    }

    private void CheckForCompletion()
    {
        foreach (var target in _targets)
        {
            if (!_targets[target.Key])
            {
                return;
            }
        }
        
        QuestManager.Instance.ActiveQuest.CompleteObjective();
        QuestManager.Instance.UpdateUI();
    }

    public void CompleteObjective()
    {
        QuestManager.Instance.ActiveQuest.CompleteObjective();
        QuestManager.Instance.UpdateUI();
    }
}
