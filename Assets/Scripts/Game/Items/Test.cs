using System;
using Game.Character;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Items
{
    [Serializable]
    public class Test : Item
    {
        public Test(string itemName, string iconPath, bool interactable) : base(itemName, iconPath, interactable)
        {
        }
        
        public override void Use()
        {
            Debug.Log("Using item " + ItemName);
            Object.FindObjectOfType<PlayerController>().GetComponent<Health>().TakeDamage(10f, false);
        }
    }
}