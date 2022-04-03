using System;
using System.Collections;
using System.Collections.Generic;
using Game.Dialogue;
using Game.Interaction;
using UnityEngine;

namespace Game.Questing
{
    [System.Serializable]
    public class Quest
    {
        [SerializeField] private string uniqueIdentifier;
        [SerializeField] private Speaker giver;
        [SerializeField] private string title, description;
        [SerializeField] private List<GameObject> questObjects;

        [SerializeField] private List<Objective> objectives;
        private int _objectiveIndex;

        private bool _isCompleted = false;
        
        public bool IsCompleted => _isCompleted;

        public string UniqueIdentifier => uniqueIdentifier;
        public Speaker Giver => giver;
        public string Title => title;
        public string Description => description;
        public List<Objective> Objectives => objectives;

        public void Init()
        {
            foreach (var questObject in questObjects)
            {
                questObject.SetActive(true);
            }
            
            foreach (var objective in Objectives)
            {
                objective.Init();
            }
            
            objectives[_objectiveIndex].OnObjectiveActivated();
        }

        public void CleanUp()
        {
            foreach (var questObject in questObjects)
            {
                questObject.SetActive(false);
            }
        }

        public Objective GetCurrentObjective()
        {
            return objectives[_objectiveIndex];
        }

        public void CompleteObjective()
        {
            objectives[_objectiveIndex].OnObjectiveDeactivated();
            
            if (_objectiveIndex + 1 >= objectives.Count)
            {
                CompleteQuest();
            }
            else
            {
                _objectiveIndex++;
                objectives[_objectiveIndex].OnObjectiveActivated();
            }
        }

        public void CompleteQuest()
        {
            Debug.Log("Completed quest");
            _isCompleted = true;
            if (QuestManager.Instance.ActiveQuest == this)
            {
                QuestManager.Instance.RemoveQuest();
            }
        }
    }
}