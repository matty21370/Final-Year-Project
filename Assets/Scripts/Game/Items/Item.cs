using System;

namespace Game.Items
{
    [Serializable]
    public abstract class Item
    {
        private string _itemName;
        private string _itemDescription, _onUseText;
        private string _iconPath;
        private bool _interactable;
        private bool _stackable;
        
        protected ItemTypes ItemType;

        public string ItemName => _itemName;
        public string ItemDescription => _itemDescription;
        public string OnUseText => _onUseText;
        public string IconPath => _iconPath;
        public bool Interactable => _interactable;
        public bool Stackable => _stackable;
        
        public Item(string itemName, string itemDescription, string onUseText, string iconPath, bool interactable, bool stackable)
        {
            _itemName = itemName;
            _itemDescription = itemDescription;
            _onUseText = onUseText;
            _iconPath = iconPath;
            _interactable = interactable;
            _stackable = stackable;
        }
        public ItemTypes GetItemType()
        {
            return ItemType;
        }
        
    }

    public enum ItemTypes
    {
        Consumable,
        Weapon,
        Equipment,
        Resource
    }
}


