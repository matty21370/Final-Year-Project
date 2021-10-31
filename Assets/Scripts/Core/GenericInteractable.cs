using Game.Character;
using UnityEngine;

namespace Game.Core
{
    public class GenericInteractable : Interactable
    {
        public override void OnInteract(Interactor interactor)
        {
            base.OnInteract(interactor);
            print("Interacting with Generic Interactable!");
        }
    }
}