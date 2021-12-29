using UnityEngine;

namespace Game.Core.Inventory
{
    [CreateAssetMenu(fileName = "Item", order = 1)]
    public class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private bool interactable;

        public string ItemName => itemName;
        public Sprite Icon => icon;
        public bool Interactable => interactable;
    }
}