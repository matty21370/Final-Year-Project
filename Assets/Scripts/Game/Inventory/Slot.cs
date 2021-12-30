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
            slotIcon.sprite = item.Icon;
            var tmp = slotIcon.color;
            tmp.a = 1f;
            slotIcon.color = tmp;
        }

        public bool Empty()
        {
            return _itemInSlot == null;
        }
    
    }
}


