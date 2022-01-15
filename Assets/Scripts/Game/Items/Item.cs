using System;

namespace Game.Items
{
    [Serializable]
    public abstract class Item
    {
        private string _itemName;
        private string _itemDescription, _onUse;
        private string _iconPath;
        private bool _interactable;

        public string ItemName => _itemName;
        public string ItemDescription => _itemDescription;
        public string OnUse => _onUse;
        public string IconPath => _iconPath;
        public bool Interactable => _interactable;

        public Item(string itemName, string itemDescription, string onUse, string iconPath, bool interactable)
        {
            _itemName = itemName;
            _itemDescription = itemDescription;
            _onUse = onUse;
            _iconPath = iconPath;
            _interactable = interactable;
        }
        public abstract void Use();
        
    }
}


