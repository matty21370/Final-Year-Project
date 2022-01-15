using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Items;
using UnityEngine.EventSystems;

namespace Game.Inventory
{
    public class Slot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image slotIcon;

        private InventorySystem _inventory;
        private Item _itemInSlot;

        private void Awake()
        {
            _inventory = FindObjectOfType<InventorySystem>();
        }

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
            if(_itemInSlot == null) return;
            
            _inventory.RemoveItem(_itemInSlot);
            _itemInSlot = null;
            slotIcon.sprite = null;
            HandleIcon(true);
        }

        public bool Empty()
        {
            return _itemInSlot == null;
        }

        private void OnLeftClick()
        {
            FindObjectOfType<ItemInfo>().SetItem(_itemInSlot);
        }

        private void OnRightClick()
        {
            _itemInSlot.Use();
            RemoveItem();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_itemInSlot == null) return;
            
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                print("Left click");
                OnLeftClick();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                
            }
        }
    }
}


