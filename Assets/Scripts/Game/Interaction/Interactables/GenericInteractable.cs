using Game.Character;
using UnityEngine;

namespace Game.Interaction.Interactables
{
    public class GenericInteractable : Interactable
    {
        public override void OnInteract(Interactor interactor)
        {
            base.OnInteract(interactor);
            Interacted = false;
        }
    }
}