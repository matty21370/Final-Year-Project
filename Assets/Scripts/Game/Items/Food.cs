using System;
using Game.Character;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Items
{
    [Serializable]
    public class Food : Item
    {
        private float _healAmt;
        
        public Food(string itemName, string itemDescription, string iconPath, float healAmt) : base(itemName, itemDescription, "Restores " + healAmt + " health", iconPath, true)
        {
            _healAmt = healAmt;
        }
        
        public override void Use()
        {
            Object.FindObjectOfType<PlayerController>().GetComponent<Health>().TakeDamage(_healAmt, true);
        }
    }
}