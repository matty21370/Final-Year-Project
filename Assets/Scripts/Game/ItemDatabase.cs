using System;
using System.Collections.Generic;
using Game.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ItemDatabase : MonoBehaviour
    {
        [SerializeField] private List<Item> items = new List<Item>();

        private void Awake()
        {
            items.Add(new Test("Test", "", true));
        }

        public Item GetItem(string n)
        {
            foreach (var item in items)
            {
                if (item.ItemName == n)
                {
                    return item;
                }
            }

            Debug.LogWarning("ItemDatabase: Could not find item " + n);
            return null;
        }
    }
}