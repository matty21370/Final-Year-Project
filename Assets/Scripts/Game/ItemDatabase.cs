using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Game.Character;
using Game.Inventory;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ItemDatabase : MonoBehaviour
    {
        [SerializeField] private List<Item> items;

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

    [System.Serializable]
    public class Item
    {
        [SerializeField] private string itemName;
        [SerializeField] private string iconPath;
        [SerializeField] private bool interactable;
        [Header("If applicable")]
        [SerializeField] private string interactMethod;

        [SerializeField] private string parameter1;
        [SerializeField] private string parameter2;
        [SerializeField] private string parameter3;
        [SerializeField] private string parameter4;

        private MethodInfo _methodInfo;

        public string ItemName => itemName;
        public string IconPath => iconPath;
        public bool Interactable => interactable;

        public Item()
        {
            if (interactable)
            {
                _methodInfo = GetType().GetMethod(interactMethod);
            }
        }

        public void Interact()
        {
            _methodInfo.Invoke(this, null);
        }

        public void AddHealth()
        {
            Debug.Log("Adding health");
        }
    }
}