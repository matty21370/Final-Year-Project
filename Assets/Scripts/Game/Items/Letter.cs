using Game.Questing;
using Game.UI;
using UnityEngine;

namespace Game.Items
{
    public class Letter : InteractableItem
    {
        private string letterContents;

        public string LetterContents => letterContents;
        
        public Letter(string itemName, string letterContents) : base(itemName, "A letter; wonder what's inside?", "Read the letter", null, true, false, false)
        {
            this.letterContents = letterContents;
            ItemType = ItemTypes.Interactable;
        }

        public override void OnUse()
        {
            base.OnUse();
            UILetter.Instance.OpenLetter(letterContents);
        }
    }
}