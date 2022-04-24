using System;
using System.Collections.Generic;
using Game.Questing;
using UnityEngine;

namespace Game.UI
{
    public class QuestMenu : MonoBehaviour
    {
        [SerializeField] private GameObject questParent, questPrefab;

        private QuestManager _questManager;

        private List<GameObject> _currentItems;

        public static QuestMenu QuestMenuInstance;

        private void Awake()
        {
            QuestMenuInstance = this;
        }

        private void OnEnable()
        {
            Init();
        }

        private void Start()
        {
            _currentItems = new List<GameObject>();
            _questManager = QuestManager.Instance;
        }

        private void ResetMenu()
        {
            foreach (var item in _currentItems)
            {
                Destroy(item);
            }
            
            _currentItems.Clear();
        }

        public void Init()
        {
            ResetMenu();
            
            foreach (var quest in _questManager.CurrentQuests)
            {
                GameObject questObject = Instantiate(questPrefab, questParent.transform);
                _currentItems.Add(questObject);
                QuestItem questItem = questObject.GetComponent<QuestItem>();
                questItem.Init(quest);
            }
        }
    }
}