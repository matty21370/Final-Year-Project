namespace Game.Items
{
    [System.Serializable]
    public class Resource : Item
    {
        public Resource(string itemName, string itemDescription, string iconPath) : base(itemName, itemDescription, "", iconPath, false)
        {
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}