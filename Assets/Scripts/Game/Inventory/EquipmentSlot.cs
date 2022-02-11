using System;
using Game.Items;
using Game.Items.Weapons;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class EquipmentSlot : MonoBehaviour
    {
        [SerializeField] private int index;
        private IUsable _itemInSlot;

        [SerializeField] private Image iconSprite;

        public IUsable ItemInSlot => _itemInSlot;

        public void UpdateSlot(Item item)
        {
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
        }

        public void UseItem()
        {
            print("fff");
            
            if(_itemInSlot == null) return;

            _itemInSlot.OnUse();
            ClearSlot();
        }
    }
}