using System;
using System.Collections.Generic;
using Game.Saving;
using Game.Items;
using UnityEngine;

namespace Game.Inventory
{
    public class InventorySystem : MonoBehaviour, ISaveable
    {
        private static InventorySystem _instance;
        public static InventorySystem Instance => _instance;
        
        [SerializeField] private int numSlots;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject panel1;

        private List<Slot> _slots = new List<Slot>();
        //private List<Item> _items = new List<Item>();

        [SerializeField] private EquipmentSlot[] equipmentSlots;
        [SerializeField] private WeaponSlot[] weaponSlots;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < numSlots; i++)
            {
                GameObject slot = Instantiate(slotPrefab, panel1.transform);
                _slots.Add(slot.GetComponent<Slot>());
            }
        }

        public void AddItem(Item item)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i].Empty())
                {
                    _slots[i].SetItem(item);
                    //_items.Add(item);
                    return;
                }
            }
        }

        public void AddItem(Item item, int amt)
        {
            foreach (var slot in _slots)
            {
                if (item.Stackable)
                {
                    if (slot.ItemInSlot == item)
                    {
                        slot.AddItem(amt);
                        return;
                    }
                }

                if (slot.Empty())
                {
                    slot.SetItem(item, amt);
                    return;
                }
            }
        }

        public void RemoveItem(Item item)
        {
            //_items.Remove(item);
        }

        private void ClearInventory()
        {
            foreach (var slot in _slots)
            {
                slot.RemoveItem();
            }
        }

        public EquipmentSlot GetEquipmentSlot(int index)
        {
            return equipmentSlots[index];
        }

        public WeaponSlot GetWeaponSlot(int index)
        {
            return weaponSlots[index];
        }

        public object CaptureState()
        {
            var items = new List<Tuple<int, Item>>();
            for (int i = 0; i < _slots.Count; i++)
            {
                items.Add(Tuple.Create(i, _slots[i].ItemInSlot));
            }

            return items;
        }

        public void RestoreState(object state)
        {
            ClearInventory();
            var items = (List<Tuple<int, Item>>) state;
            foreach (var item in items)
            {
                _slots[item.Item1].SetItem(item.Item2);
            }
        }
    }

}

