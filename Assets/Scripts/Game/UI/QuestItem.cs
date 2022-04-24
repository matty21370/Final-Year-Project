using Game.Questing;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class QuestItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;

        private Quest _quest;
        
        public void Init(Quest quest)
        {
            titleText.text = quest.Title;
            _quest = quest;
        }

        public void OnClicked()
        {
            QuestManager.Instance.SetActiveQuest(_quest);
        }
    }
}