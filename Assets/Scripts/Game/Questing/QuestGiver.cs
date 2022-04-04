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
            print("Adding quest: " + quest.Title);
            QuestManager.Instance.AddQuestToJournal(quest);
        }

        private void Awake()
        {
            foreach (var quest in _questsToGive)
            {
                QuestManager.Instance.RegisterQuest(quest);
            }
        }
    }
}