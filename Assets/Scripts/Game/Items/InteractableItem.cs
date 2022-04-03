using Game.Questing;

namespace Game.Items
{
    public class InteractableItem : Item, IUsable
    {
        public InteractableItem(string itemName, string itemDescription, string onUseText, string iconPath, bool interactable, bool stackable, bool consumable) : base(itemName, itemDescription, "", iconPath, interactable, stackable, consumable)
        {
        }

        public virtual void OnUse()
        {
            if (QuestManager.Instance.ActiveQuest != null)
            {
                Objective currentObjective = QuestManager.Instance.ActiveQuest.GetCurrentObjective();
                if (currentObjective.Goal == Objective.Goals.Use)
                {
                    if (currentObjective.ItemObjective != null)
                    {
                        if (currentObjective.ItemObjective == this)
                        {
                            currentObjective.CompleteObjective();
                        }
                    }
                }
            }
        }
    }
}