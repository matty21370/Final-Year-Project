using Game.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class ArmourSlot : MonoBehaviour
    {
        private Armour _armourInSlot;

        [SerializeField] private Image icon;

        public void SetArmour(Armour armour)
        {
            if (_armourInSlot != null)
            {
                InventorySystem.Instance.AddItem(_armourInSlot);
            }

            _armourInSlot = armour;
            icon.sprite = Resources.Load<Sprite>(armour.IconPath);
        }
    }
}