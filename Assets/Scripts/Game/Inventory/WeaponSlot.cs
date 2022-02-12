using System;
using Game.Items.Weapons;
using Game.Saving;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class WeaponSlot : MonoBehaviour, ISaveable
    {
        [SerializeField] private Image iconImage;
        
        private Weapon _weaponInSlot;

        public Weapon WeaponInSlot => _weaponInSlot;

        private void Awake()
        {
            HandleColour(false);
        }

        public void SetWeapon(Weapon weapon)
        {
            if(weapon == null) return;
            
            _weaponInSlot = weapon;
            iconImage.sprite = Resources.Load<Sprite>(weapon.IconPath);
            HandleColour(true);
        }

        public void RemoveWeapon()
        {
            _weaponInSlot = null;
            iconImage.sprite = null;
            HandleColour(false);
        }

        public void HandleColour(bool show)
        {
            var col = iconImage.color;
            col.a = show ? 1 : 0;
            iconImage.color = col;
        }

        public void OnClick()
        {
            if(_weaponInSlot == null) return;
            
            InventorySystem.Instance.AddItem(_weaponInSlot);
            RemoveWeapon();
        }

        public object CaptureState()
        {
            return _weaponInSlot;
        }

        public void RestoreState(object state)
        {
            SetWeapon((Weapon) state);
        }
    }
}