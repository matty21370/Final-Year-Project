using Game.Character;
using UnityEngine;

namespace Game.Core
{
    public class GenericInteractable : Interactable
    {
        public override void OnInteract(Interactor interactor)
        {
            interactor.GetComponent<Health>().TakeDamage(10);
            base.OnInteract(interactor);
        }
    }
}