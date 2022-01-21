using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items.Weapons
{
    [Serializable]
    public class Weapon : Item
    {
        private float _damage;
        private float _range;

        private string _model;

        public enum WeaponTypes {Unarmed, Sword, Shield}
        private WeaponTypes _weaponType;

        public float Damage => _damage;
        public float Range => _range;
        public WeaponTypes WeaponType => _weaponType;
        public string Model => _model;
        
        public Weapon(string itemName, string itemDescription, string onUse, string iconPath, float damage, float range, WeaponTypes weaponType, string modelPath) : base(itemName, itemDescription, onUse, iconPath, false)
        {
            _damage = damage;
            _range = range;
            _weaponType = weaponType;
            if (modelPath != null)
            {
                _model = modelPath;
            }

            ItemType = ItemTypes.Weapon;
        }

        public bool HasModel()
        {
            return _model != null;
        }

        public GameObject GetModel()
        {
            return ModelLoader.Instance.GetModel(_model);
        }

        public override void Use()
        {
            throw new NotImplementedException();
        }
    }
}

