using System.Collections.Generic;
using Game.Items;
using UnityEngine;

namespace Game
{
    public class ItemDatabase : MonoBehaviour
    {
        private readonly List<Item> _items = new List<Item>();

        private void Awake()
        {
            _items.Add(new Food("Test food", "This is food added for testing purposes.", "Sprites/Food", 20));
        }

        public Item GetItem(string itemName)
        {
            foreach (var item in _items)
            {
                if (item.ItemName.ToLower() == itemName.ToLower())
                {
                    return item;
                }
            }

            Debug.LogWarning("ItemDatabase: Could not find item " + itemName);
            return null;
        }
    }
}