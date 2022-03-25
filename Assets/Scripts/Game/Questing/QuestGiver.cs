using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Questing
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private List<Quest> _questsToGive = new List<Quest>();

        public void ActivateQuest(string id)
        {
            Quest quest = QuestManager.Instance.GetQuestFromID(id);
            QuestManager.Instance.AddQuestToJournal(quest);
            print("Added quest: " + quest.Title);
        }

        private void Start()
        {
            foreach (var quest in _questsToGive)
            {
                QuestManager.Instance.RegisterQuest(quest);
            }
        }
    }
}