using System.Collections;
using System.Collections.Generic;
using Game.Interaction;
using Game.Questing;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public enum Goals
    {
        INTERACT,
        KILL
    }
    
    [SerializeField] private string description;
    
    [SerializeField] private List<Interactable> targets;
    private Dictionary<Interactable, bool> _targets;
    [SerializeField] private Goals goal;

    public Goals Goal => goal;
    public List<Interactable> Targets => targets;
    public string Description => description;

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
            _targets.Add(interactable, false);
        }
    }

    public void CompleteTarget(Interactable target)
    {
        if (_targets.ContainsKey(target))
        {
            QuestManager.Instance.UpdateUI();
            _targets[target] = true;
            Debug.Log("Target completed");
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
        
        Debug.Log("Objective completed");
        QuestManager.Instance.ActiveQuest.CompleteObjective();
        QuestManager.Instance.UpdateUI();
    }
}
