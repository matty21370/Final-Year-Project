using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Items;
using Game.Saving;
using UnityEngine.EventSystems;

namespace Game.Inventory
{
    public class Slot : MonoBehaviour, IPointerClickHandler, ISaveable
    {
        [SerializeField] private Image slotIcon;
        [SerializeField] private Text amountText;
        
        private InteractionMenu _interactionMenu;
        private ItemInfo _itemInfo;
        private Item _itemInSlot;
        private int amt;
        private IUsable _usable;

        public Item ItemInSlot => _itemInSlot;
        public IUsable Usable => _usable;
        public int ItemAmount => amt;

        public void Init(InteractionMenu menu)
        {
            _interactionMenu = menu;
        }

        public void SetItem(Item item)
        {
            _itemInSlot = item;
            if(_itemInSlot is IUsable)
                _usable = (IUsable) item;
            slotIcon.sprite = Resources.Load<Sprite>(item.IconPath);
            HandleIcon(false);
        }

        public void SetItem(Item item, int amount)
        {
            SetItem(item);
            amt = amount;
            //amountText.text = amt.ToString();
        }

        public void AddItem(int amount)
        {
            amt += amount;
            //amountText.text = amt.ToString();
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
            //amountText.text = "";
            HandleIcon(true);
        }

        public bool Empty()
        {
            return _itemInSlot == null;
        }

        private void OnLeftClick()
        {
            if(_itemInSlot == null) return;
            FindObjectOfType<ItemInfo>().SetSlot(this);
        }

        private void OnRightClick()
        {
            if(_itemInSlot == null) return;
            FindObjectOfType<ItemInfo>().SetSlot(this);
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

        public object CaptureState()
        {
            return _itemInSlot;
        }

        public void RestoreState(object state)
        {
            //if(state == null) return;
            
            RemoveItem();
            SetItem(state as Item);
        }
    }
}


