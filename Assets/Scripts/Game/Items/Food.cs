using System;
using Game.Character;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Items
{
    [Serializable]
    public class Food : Item
    {
        public Food(string itemName, string iconPath, bool interactable) : base(itemName, iconPath, interactable)
        {
        }
        
        public override void Use()
        {
            Object.FindObjectOfType<PlayerController>().GetComponent<Health>().TakeDamage(20f, true);
        }
    }
}