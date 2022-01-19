using System;
using Game.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class ItemInfo : MonoBehaviour
    {
        [SerializeField] private Text itemNameText;
        [SerializeField] private Text itemDescriptionText;
        [SerializeField] private Text onUse, onUseText;

        private void Awake()
        {
            ResetInfo();
        }

        public void ResetInfo()
        {
            itemNameText.text = "No item selected";
            itemDescriptionText.text = "";
            onUseText.text = "";
            OnUpdate();
        }

        public void SetItem(Item item)
        {
            itemNameText.text = item.ItemName;
            itemDescriptionText.text = item.ItemDescription;
            onUseText.text = item.OnUse;
            OnUpdate();
        }

        private void OnDisable()
        {
            ResetInfo();
        }

        private void OnUpdate()
        {
            onUse.gameObject.SetActive(onUseText.text != "");
        }
    }
}