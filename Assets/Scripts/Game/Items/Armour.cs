using Game.Character;

namespace Game.Items
{
    public class Armour : Item
    {
        private ArmourSet _armourSet;

        public ArmourSet ArmourSet => _armourSet;

        public Armour(string itemName, ArmourSet armourSet, string itemDescription, string onUseText, string iconPath, bool interactable, bool consumable) : base(itemName, itemDescription, onUseText, iconPath, interactable, false, consumable)
        {
            _armourSet = armourSet;

            ItemType = ItemTypes.Equipment;
        }
        
        
    }
}