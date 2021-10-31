using Game.Character;
using UnityEngine;

namespace Game.Core
{
    public class GenericInteractable : Interactable
    {
        public override void OnInteract(Interactor interactor)
        {
            
            print("Interacting with Generic Interactable!");
            base.OnInteract(interactor);
        }
    }
}