using Game.Character;
using UnityEngine;

namespace Game.Interaction.Interactables
{
    public class GenericInteractable : Interactable
    {
        public override void OnInteract(Interactor interactor)
        {
            interactor.GetComponent<Health>().TakeDamage(10, false);
            base.OnInteract(interactor);
        }
    }
}