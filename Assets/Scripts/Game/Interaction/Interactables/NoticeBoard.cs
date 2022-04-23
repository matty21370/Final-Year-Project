using System;
using Game.Questing;
using Game.UI;
using UnityEngine;

namespace Game.Interaction.Interactables
{
    public class NoticeBoard : Interactable
    {
        [SerializeField] private NoticeBoardItem[] noticeBoardItems;
        
        private void Start()
        {
            Invoke(nameof(Init), 1f);
        }

        private void Init()
        {
            foreach (var noticeBoardItem in noticeBoardItems)
            {
                noticeBoardItem.Init();
            }
        }

        public override void OnInteract(Interactor interactor)
        {
            if(!interactor.GetIsPlayer()) return;
            
            UIManager.Instance.ShowNoticeBoard(noticeBoardItems);
            
            base.ResetInteractable(interactor);
        }
    }

    [Serializable]
    public class NoticeBoardItem
    {
        [SerializeField] private string questId;
        private Quest _quest;

        [SerializeField] private string title, description;
        
        [SerializeField] private QuestGiver questGiver;

        public string QuestId => questId;
        public Quest Quest => _quest;
        public string Title => title;
        public string Description => description;

        public QuestGiver QuestGiver => questGiver;

        public void Init()
        {
            _quest = QuestManager.Instance.GetQuestFromID(questId);
        }
    }
}