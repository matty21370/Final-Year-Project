using System;
using Game.Items.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class WeaponSlot : MonoBehaviour
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
            print("Setting weapon " + weapon.ItemName);
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
    }
}