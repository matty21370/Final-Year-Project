using System;

namespace Game.Items
{
    [Serializable]
    public abstract class Item
    {
        private string _itemName;
        private string _iconPath;
        private bool _interactable;

        public string ItemName => _itemName;
        public string IconPath => _iconPath;
        public bool Interactable => _interactable;

        public Item(string itemName, string iconPath, bool interactable)
        {
            _itemName = itemName;
            _iconPath = iconPath;
            _interactable = interactable;
        }
        public abstract void Use();
    }
}


