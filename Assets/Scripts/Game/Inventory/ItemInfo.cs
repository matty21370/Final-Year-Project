using System;
using Game.Character;
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

        private Item _contextItem;

        private PlayerController _playerController;
        
        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
            ResetInfo();
        }

        private void Update()
        {
            if(_contextItem == null) return;

            if (Input.GetKeyDown(KeyCode.W))
            {
                
            }
        }

        public void ResetInfo()
        {
            _contextItem = null;
            itemNameText.text = "No item selected";
            itemDescriptionText.text = "";
            onUseText.text = "";
            OnUpdate();
        }

        public void SetItem(Item item)
        {
            _contextItem = item;
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