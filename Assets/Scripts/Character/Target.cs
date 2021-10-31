using Game.Core;

namespace Game.Character
{
    public class Target : Interactable
    {
        public override void OnInteract(Interactor interactor)
        {
            base.OnInteract(interactor);
            interactor.GetComponent<Combat>().SetTarget(this);
        }
    }
}