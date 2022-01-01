using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            var tmp = slotIcon.color;
            tmp.a = 1f;
            slotIcon.color = tmp;
        }

        public void RemoveItem()
        {
            _itemInSlot = null;
            slotIcon.sprite = null;
        }

        public bool Empty()
        {
            return _itemInSlot == null;
        }

        public void OnClick()
        {
            
        }
    
    }
}


