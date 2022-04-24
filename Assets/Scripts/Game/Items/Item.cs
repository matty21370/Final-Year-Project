using System;
using UnityEngine;

namespace Game.Items
{
    [Serializable]
    public class Item
    {
        private string _itemName;
        private string _itemDescription, _onUseText;
        private string _iconPath;
        private bool _interactable;
        private bool _stackable;
        private bool _consumable;
        
        public ItemTypes ItemType;

        public string ItemName => _itemName;
        public string ItemDescription => _itemDescription;
        public string OnUseText => _onUseText;
        public string IconPath => _iconPath;
        public bool Interactable => _interactable;
        public bool Stackable => _stackable;
        public bool Consumable => _consumable;
        
        public Item(string itemName, string itemDescription, string onUseText, string iconPath, bool interactable, bool stackable, bool consumable)
        {
            _itemName = itemName;
            _itemDescription = itemDescription;
            _onUseText = onUseText;
            _iconPath = iconPath;
            _interactable = interactable;
            _stackable = stackable;
            _consumable = consumable;
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
        Resource,
        Interactable,
        Armour
    }
}


