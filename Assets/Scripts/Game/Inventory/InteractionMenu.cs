using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Items;
using UnityEngine;

namespace Game.Inventory
{
    public class InteractionMenu : MonoBehaviour
    {
        private Slot _contextSlot;

        [SerializeField] private Vector2 offset;

        private void Awake()
        {
            Slot[] slots = FindObjectsOfType<Slot>();
            foreach (var s in slots)
            {
                s.Init(this);
            }
            gameObject.SetActive(false);
        }

        public void SetSlot(Slot slot)
        {
            if(!slot.ItemInSlot.Interactable) return;
            transform.position = Input.mousePosition + new Vector3(offset.x, offset.y, 0);
            _contextSlot = slot;
            gameObject.SetActive(true);
        }

        public void UseItemClicked()
        {
            if(_contextSlot.ItemInSlot == null) return;

            _contextSlot.ItemInSlot.Use();
            _contextSlot.RemoveItem();
            _contextSlot = null;
            gameObject.SetActive(false);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}