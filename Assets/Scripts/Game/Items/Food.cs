using System;
using Game.Character;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Items
{
    [Serializable]
    public class Food : Item, IUsable
    {
        private float _healAmt;
        
        public Food(string itemName, string itemDescription, string iconPath, float healAmt) : base(itemName, itemDescription, "Restores " + healAmt + " health", iconPath, true, true, true)
        {
            _healAmt = healAmt;
            ItemType = ItemTypes.Consumable;
        }
        
        public void OnUse()
        {
            Object.FindObjectOfType<PlayerController>().GetComponent<Health>().Heal(_healAmt);
        }
    }
}