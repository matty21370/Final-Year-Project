using System.Collections;
using System.Collections.Generic;
using Game.Saving;
using UnityEngine;

namespace Game.Inventory
{
    public class InventorySystem : MonoBehaviour, ISaveable
    {
        [SerializeField] private int numSlots;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject panel1;

        private List<Slot> _slots = new List<Slot>();
        private List<Item> _items = new List<Item>();

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
                    _items.Add(item);
                    print("Added item to inventory.");
                    return;
                }
            }
        }

        public void ClearInventory()
        {
            foreach (var slot in _slots)
            {
                slot.RemoveItem();
            }
            
            _items.Clear();
        }

        public object CaptureState()
        {
            return _items.ToArray();
        }

        public void RestoreState(object state)
        {
            Item[] data = (Item[]) state;
            
            ClearInventory();
            
            foreach (var item in data)
            {
                AddItem(item);
            }
        }
    }

}

