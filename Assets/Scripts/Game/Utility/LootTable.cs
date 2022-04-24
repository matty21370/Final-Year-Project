using System;
using System.Collections.Generic;
using Game.Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Utility
{
    public class LootTable : MonoBehaviour
    {
        private static LootTable _instance;
        public static LootTable Instance => _instance;

        private List<Item> _tier1 = new List<Item>();
        private List<Item> _tier2 = new List<Item>();
        private List<Item> _tier3 = new List<Item>();

        public List<Item> Tier1 => _tier1;

        public List<Item> Tier2 => _tier2;

        public List<Item> Tier3 => _tier3;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddItemToLootTable(Item item, int tier)
        {
            switch (tier)
            {
                case 1:
                    _tier1.Add(item);
                    break;
                case 2:
                    _tier2.Add(item);
                    break;
                case 3:
                    _tier3.Add(item);
                    break;
            }
        }

        public List<Item> GetItemsFromLootTable(int tier, int amount)
        {
            List<Item> tmp = new List<Item>();

            switch (tier)
            {
                case 1:
                    for (int i = 0; i < amount; i++)
                    {
                        tmp.Add(_tier1[Random.Range(0, _tier1.Count)]);
                    }
                    break;
                case 2:
                    for (int i = 0; i < amount; i++)
                    {
                        tmp.Add(_tier2[Random.Range(0, _tier2.Count)]);
                    }
                    break;
                case 3:
                    for (int i = 0; i < amount; i++)
                    {
                        tmp.Add(_tier3[Random.Range(0, _tier3.Count)]);
                    }
                    break;
            }

            return tmp;
        }

        public List<Item> GetItemsFromLootTable(int tier, int amount, int secondTier, int secondAmount)
        {
            List<Item> list = new List<Item>();
            List<Item> tmp1 = GetItemsFromLootTable(tier, amount);
            List<Item> tmp2 = GetItemsFromLootTable(secondTier, secondAmount);
            
            list.AddRange(tmp1);
            list.AddRange(tmp2);

            return list;
        }

        public List<Item> GetItemsFromLootTable(int firstTier, int firstAmount, int secondTier, int secondAmount,
            int thirdTier, int thirdAmount)
        {
            List<Item> list = new List<Item>();
            List<Item> tmp = GetItemsFromLootTable(firstTier, firstAmount, secondTier, secondAmount);
            List<Item> tmp2 = GetItemsFromLootTable(thirdTier, thirdAmount);
            
            list.AddRange(tmp);
            list.AddRange(tmp2);

            return list;
        }
    }
}