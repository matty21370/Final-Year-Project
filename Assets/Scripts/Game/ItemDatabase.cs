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
            _items.Add(new Test("Test", "", true));
        }

        public Item GetItem(string itemName)
        {
            foreach (var item in _items)
            {
                if (item.ItemName == itemName)
                {
                    return item;
                }
            }

            Debug.LogWarning("ItemDatabase: Could not find item " + itemName);
            return null;
        }
    }
}