using Game.Interaction.Interactables;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class NoticeBoardUIElement : MonoBehaviour
    {
        [SerializeField] private Text titleText, descriptionText;

        private NoticeBoardItem _item;

        public void Init(NoticeBoardItem item)
        {
            this._item = item;
            titleText.text = item.Title;
            descriptionText.text = item.Description;
        }

        public void OnClick()
        {
            UILetter.Instance.OpenLetter(_item.Title + "\n\n" + _item.Description);
        }
    }
}