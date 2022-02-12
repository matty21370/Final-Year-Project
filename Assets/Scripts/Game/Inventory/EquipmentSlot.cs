using System;
using Game.Items;
using Game.Items.Weapons;
using Game.Saving;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class EquipmentSlot : MonoBehaviour, ISaveable
    {
        [SerializeField] private int index;
        private IUsable _itemInSlot;

        [SerializeField] private Image iconSprite;

        public IUsable ItemInSlot => _itemInSlot;

        public void UpdateSlot(Item item)
        {
            if(item == null) return;
            
            if(!(item is IUsable usable)) return;
            _itemInSlot = usable;
            iconSprite.sprite = Resources.Load<Sprite>(item.IconPath);
            var tmp = iconSprite.color;
            tmp.a = 1;
            iconSprite.color = tmp;
        }

        public void ClearSlot()
        {
            iconSprite.sprite = null;
            var tmp = iconSprite.color;
            tmp.a = 0;
            iconSprite.color = tmp;
            _itemInSlot = null;
        }

        public void UseItem()
        {
            if(_itemInSlot == null) return;

            _itemInSlot.OnUse();
            ClearSlot();
                }
        
        public void OnClick()
        {
            if(_itemInSlot == null) return;
            
            InventorySystem.Instance.AddItem(_itemInSlot as Item);
            ClearSlot();
        }

        public object CaptureState()
        {
            return _itemInSlot;
        }

        public void RestoreState(object state)
        {
            UpdateSlot((Item) state);
        }
    }
}