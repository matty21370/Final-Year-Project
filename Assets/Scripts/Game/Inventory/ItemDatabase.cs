using System.Collections.Generic;
using Game.Items;
using Game.Items.Weapons;
using UnityEngine;

namespace Game
{
    public class ItemDatabase : MonoBehaviour
    {
        private static ItemDatabase _instance;
        public static ItemDatabase Instance => _instance;
        
        private readonly List<Item> _items = new List<Item>();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            
            Init();
        }

        private void Init()
        {
            _items.Add(new Food("Test food", "This is food added for testing purposes.", "Sprites/Food", 20, false));
            _items.Add(new Items.Weapons.Weapon("Unarmed", "no", "Deals damage", null, 10f, 1.5f, Items.Weapons.Weapon.WeaponTypes.Unarmed, null));
            _items.Add(new Items.Weapons.Weapon("Hammer", "An amazing hammer", "Deals damage", "Sprites/Hammer", 20f, 1.5f, Items.Weapons.Weapon.WeaponTypes.Sword, "Test"));
            _items.Add(new Resource("Wood", "Used for crafting.", "Sprites/Wood"));
            _items.Add(new Letter("Letter from Bob", "Dear player,\n\nPlease speak to me as soon as you arrive on the island. We are in a bad way, many people need your help.\n\nSafe travels,\n\nBob"));
            _items.Add(new Food("The Potion of Life", "Take it if you want eternal life", "Sprites/potion_of_life", 10000.0f, true));
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