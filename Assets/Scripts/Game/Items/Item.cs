using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Items
{
    [System.Serializable]
    public abstract class Item
    {
        [SerializeField] private string itemName;
        [SerializeField] private string iconPath;
        [SerializeField] private bool interactable;

        public string ItemName => itemName;
        public string IconPath => iconPath;
        public bool Interactable => interactable;

        public Item(string itemName, string iconPath, bool interactable)
        {
            this.itemName = itemName;
            this.iconPath = iconPath;
            this.interactable = interactable;
        }
        public abstract void Use();
    }
}


