using System.Collections.Generic;
using Game.Items;
using Game.Items.Weapons;
using UnityEngine;

namespace Game
{
    public class ItemDatabase : MonoBehaviour
    {
        private readonly List<Item> _items = new List<Item>();

        private void Awake()
        {
            _items.Add(new Food("Test food", "This is food added for testing purposes.", "Sprites/Food", 20));
            _items.Add(new Weapon("Unarmed", "no", "Deals damage", null, 10f, 1.5f, Weapon.WeaponTypes.Unarmed));
            _items.Add(new Resource("Wood", "Used for crafting.", "Sprites/Wood"));
        }

        public Item GetItem()
        {
            int i = Random.Range(0, _items.Count);
            if (_items[i].ItemName != "Unarmed")
            {
                return _items[i];
            }

            return GetItem();
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