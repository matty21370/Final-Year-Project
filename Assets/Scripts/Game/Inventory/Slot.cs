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

        private InteractionMenu _interactionMenu;
        private ItemInfo _itemInfo;
        private Item _itemInSlot;

        public Item ItemInSlot => _itemInSlot;

        public void Init(InteractionMenu menu)
        {
            _interactionMenu = menu;
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
            
            InventorySystem.Instance.RemoveItem(_itemInSlot);
            ItemInfo itemInfo = FindObjectOfType<ItemInfo>();
            if(itemInfo != null) itemInfo.ResetInfo();
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
            if(_itemInSlot == null) return;
            FindObjectOfType<ItemInfo>().SetItem(_itemInSlot);
        }

        private void OnRightClick()
        {
            if(_itemInSlot == null) return;
            FindObjectOfType<ItemInfo>().SetItem(_itemInSlot);
            _interactionMenu.SetSlot(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _interactionMenu.Hide();
                OnLeftClick();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick();
            }
        }
    }
}


