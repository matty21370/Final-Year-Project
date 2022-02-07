using System;
using Game.Inventory;
using Game.Items;
using UnityEngine;

namespace Game.Interaction.Interactables
{
    public class Harvestable : Interactable
    {
        [SerializeField] private string itemToGive;
        private Item _itemToGive;

        private void Start()
        {
            _itemToGive = ItemDatabase.Instance.GetItem(itemToGive);
        }

        public override void OnInteract(Interactor interactor)
        {
            base.OnInteract(interactor);
            
            Interacted = false;
            
            if(!interactor.GetIsPlayer()) return;

            InventorySystem.Instance.AddItem(_itemToGive);
        }
    }
}