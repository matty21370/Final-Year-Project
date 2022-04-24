using System.Collections;
using System.Collections.Generic;
using Game.Interaction;
using Game.Inventory;
using Game.Items;
using Game.Utility;
using UnityEngine;

public class Lootable : Interactable
{
    [SerializeField] private int tier1Items, tier2Items, tier3Items;

    public override void OnInteract(Interactor interactor)
    {
        List<Item> items = LootTable.Instance.GetItemsFromLootTable(1, tier1Items, 2, tier2Items, 3, tier3Items);

        foreach (var item in items)
        {
            InventorySystem.Instance.AddItem(item);
        }
        
        base.ResetInteractable(interactor);
    }
}
