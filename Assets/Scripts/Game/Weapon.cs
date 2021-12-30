using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Weapon", order = 1)]
    public class Weapon : ScriptableObject
    {
        public string name;
        public float damage;
        public float range;

        public enum WeaponTypes {Unarmed, Sword, Shield}
        public WeaponTypes weaponType;
    }
}

