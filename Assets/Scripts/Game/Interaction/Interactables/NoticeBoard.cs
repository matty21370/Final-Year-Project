using System;
using Game.Questing;
using Game.UI;
using UnityEngine;

namespace Game.Interaction.Interactables
{
    public class NoticeBoard : Interactable
    {
        [SerializeField] private NoticeBoardItem[] noticeBoardItems;
        
        private void Awake()
        {
            foreach (var noticeBoardItem in noticeBoardItems)
            {
                noticeBoardItem.Init();
            }
        }

        public override void OnInteract(Interactor interactor)
        {
            base.OnInteract(interactor);
            Interacted = false;
            
            UIManager.Instance.ShowNoticeBoard(noticeBoardItems);
        }
    }

    [Serializable]
    public class NoticeBoardItem
    {
        [SerializeField] private string questId;
        private Quest _quest;

        [SerializeField] private string title, description;

        public string QuestId => questId;
        public Quest Quest => _quest;
        public string Title => title;
        public string Description => description;

        public void Init()
        {
            _quest = QuestManager.Instance.GetQuestFromID(questId);
        }
    }
}