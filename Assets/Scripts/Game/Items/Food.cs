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
        private bool _poisonous;

        public bool Poisonous => _poisonous;
        
        public Food(string itemName, string itemDescription, string iconPath, float healAmt, bool poisonous) : base(itemName, itemDescription, "Restores " + healAmt + " health", iconPath, true, true, true)
        {
            _healAmt = healAmt;
            ItemType = ItemTypes.Consumable;
            _poisonous = poisonous;
        }
        
        public void OnUse()
        {
            if (_poisonous)
            {
                Debug.Log("kill me ");
                Object.FindObjectOfType<PlayerController>().GetComponent<Health>().TakeDamage(_healAmt);
            }
            else
            {
                Object.FindObjectOfType<PlayerController>().GetComponent<Health>().Heal(_healAmt);
            }
        }
    }
}