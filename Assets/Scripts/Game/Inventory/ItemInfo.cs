using System;
using Game.Character;
using Game.Items;
using Game.Items.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class ItemInfo : MonoBehaviour
    {
        [SerializeField] private Text itemNameText;
        [SerializeField] private Text itemDescriptionText;
        [SerializeField] private Text onUse, onUseText;

        private Slot _contextSlot;

        private PlayerController _playerController;
        
        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
            ResetInfo();
        }

        private void Update()
        {
            if(_contextSlot == null) return;
            if(_contextSlot.ItemInSlot == null) return;

            if (_contextSlot.ItemInSlot is IUsable)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InventorySystem.Instance.GetEquipmentSlot(0).UpdateSlot(_contextSlot.ItemInSlot);
                    _contextSlot.RemoveItem();
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    InventorySystem.Instance.GetEquipmentSlot(1).UpdateSlot(_contextSlot.ItemInSlot);
                    _contextSlot.RemoveItem();
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    InventorySystem.Instance.GetEquipmentSlot(2).UpdateSlot(_contextSlot.ItemInSlot);
                    _contextSlot.RemoveItem();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    InventorySystem.Instance.GetEquipmentSlot(3).UpdateSlot(_contextSlot.ItemInSlot);
                    _contextSlot.RemoveItem();
                }
            }

            if (_contextSlot.ItemInSlot is Weapon)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    InventorySystem.Instance.GetWeaponSlot(0).SetWeapon(_contextSlot.ItemInSlot as Weapon);
                    FindObjectOfType<PlayerController>().GetComponent<Combat>().EquipWeapon(0, _contextSlot.ItemInSlot as Weapon);
                    _contextSlot.RemoveItem();
                }
                
                if (Input.GetKeyDown(KeyCode.W))
                {
                    InventorySystem.Instance.GetWeaponSlot(1).SetWeapon(_contextSlot.ItemInSlot as Weapon);
                    FindObjectOfType<PlayerController>().GetComponent<Combat>().EquipWeapon(1, _contextSlot.ItemInSlot as Weapon);
                    _contextSlot.RemoveItem();
                }
            }
        }

        public void ResetInfo()
        {
            _contextSlot = null;
            itemNameText.text = "No item selected";
            itemDescriptionText.text = "";
            onUseText.text = "";
            OnUpdate();
        }

        public void SetSlot(Slot slot)
        {
            _contextSlot = slot;
            var item = slot.ItemInSlot;
            itemNameText.text = item.ItemName;
            itemDescriptionText.text = item.ItemDescription;
            if (item.GetItemType() == ItemTypes.Weapon)
            {
                var weapon = (Items.Weapons.Weapon) item;
                onUse.text = "Stats";
                onUseText.text = $"Damage: {weapon.Damage}\nRange: {weapon.Range}";
            }
            else if (item.GetItemType() == ItemTypes.Consumable)
            {
                var consumable = (Food) item;
                onUse.text = "When eaten:";
                string description;
                if (consumable.Poisonous)
                {
                    description = "Amazing stuff";
                }
                else
                {
                    description = consumable.OnUseText;
                }

                onUseText.text = description;
            }
            else if (item.GetItemType() == ItemTypes.Interactable)
            {
                var interactable = (Letter) item;
                onUse.text = "When used: ";
                onUseText.text = interactable.OnUseText;
            }
            else
            {
                onUse.text = "";
                onUseText.text = "";
            }
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