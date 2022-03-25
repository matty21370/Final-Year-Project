using System;
using System.Collections.Generic;
using Game.Dialogue;
using Game.Interaction;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Questing
{
    public class QuestManager : MonoBehaviour
    {
        private static QuestManager _instance;

        public static QuestManager Instance => _instance;

        private List<Quest> _allQuests = new List<Quest>();

        public void RegisterQuest(Quest quest)
        {
            _allQuests.Add(quest);
        }

        public Quest GetQuestFromID(string id)
        {
            foreach (var quest in _allQuests)
            {
                if (quest.UniqueIdentifier == id)
                {
                    return quest;
                }
            }

            return null;
        }

        private List<Quest> _currentQuests = new List<Quest>();
        private Quest _activeQuest;

        public Quest ActiveQuest => _activeQuest;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        public void SetActiveQuest(Quest quest)
        {
            _activeQuest = quest;
            foreach (var objective in quest.Objectives)
            {
                objective.Init();
            }
        }

        public void AddQuestToJournal(Quest quest)
        {
            _currentQuests.Add(quest);
            SetActiveQuest(quest);
        }
    }
}