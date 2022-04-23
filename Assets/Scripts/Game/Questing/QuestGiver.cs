using System;
using System.Collections.Generic;
using Game.UI;
using UnityEngine;

namespace Game.Questing
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private List<Quest> _questsToGive = new List<Quest>();

        public void ActivateQuest(string id)
        {
            Quest quest = QuestManager.Instance.GetQuestFromID(id);
            UIManager.Instance.ShowQuestStartedUI(quest.Title);
            QuestManager.Instance.AddQuestToJournal(quest);
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