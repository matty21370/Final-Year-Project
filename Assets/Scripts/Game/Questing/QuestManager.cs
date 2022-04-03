using System;
using System.Collections.Generic;
using Game.Dialogue;
using Game.Interaction;
using Game.Saving;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Game.Questing
{
    public class QuestManager : MonoBehaviour, ISaveable
    {
        private static QuestManager _instance;

        public static QuestManager Instance => _instance;

        private List<Quest> _allQuests = new List<Quest>();

        [SerializeField] private Text titleText, objectiveText;

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
            
            UpdateUI();
        }

        public void SetActiveQuest(Quest quest)
        {
            print(quest.Description);
            _activeQuest = quest;
            quest.Init();
            UpdateUI();
        }

        public void AddQuestToJournal(Quest quest)
        {
            _currentQuests.Add(quest);
            SetActiveQuest(quest);
        }
        
        public void UpdateUI()
        {
            if (ActiveQuest == null)
            {
                titleText.text = "No active quest";
                objectiveText.text = "";
            }
            else
            {
                titleText.text = ActiveQuest.Title;
                objectiveText.text = ActiveQuest.GetCurrentObjective().Description;
            }
            
        }

        public void RemoveQuest()
        {
            _activeQuest.CleanUp();
            _activeQuest = null;
        }

        public object CaptureState()
        {
            //if (_activeQuest != null)
            //{
            //    Dictionary<string, string> data = new Dictionary<string, string>();
            //    data.Add("quest", _activeQuest.UniqueIdentifier);
            //    data.Add("objective", _activeQuest.GetCurrentObjective().Identifier);
            //    return data;
            //}

            return null;
        }

        public void RestoreState(object state)
        {
            //if(state == null) return;

            //Dictionary<string, string> data = (Dictionary<string, string>)state;
            //SetActiveQuest(GetQuestFromID(data["quest"]));
        }
    }
}