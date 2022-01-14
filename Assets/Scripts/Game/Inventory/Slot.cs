using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Items;

namespace Game.Inventory
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image slotIcon;
    
        private Item _itemInSlot;

        public void SetItem(Item item)
        {
            _itemInSlot = item;
            slotIcon.sprite = Resources.Load<Sprite>(item.IconPath);
            HandleIcon(false);
        }

        private void HandleIcon(bool clear)
        {
            var tmp = slotIcon.color;
            tmp.a = clear ? 0f : 1f;
            slotIcon.color = tmp;
        }

        public void RemoveItem()
        {
            _itemInSlot = null;
            slotIcon.sprite = null;
            HandleIcon(true);
        }

        public bool Empty()
        {
            return _itemInSlot == null;
        }

        public void OnClick()
        {
            if(_itemInSlot == null) return;
            
            _itemInSlot.Use();
            RemoveItem();
        }
    
    }
}


