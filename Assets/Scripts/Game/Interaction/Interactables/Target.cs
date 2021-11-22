using System;
using Game.Core;

namespace Game.Character
{
    public class Target : Interactable
    {
        private Combat _combat;
        private Speaker _speaker;

        private void Awake()
        {
            _combat = GetComponent<Combat>();
            _speaker = GetComponent<Speaker>();
        }

        public override void OnInteract(Interactor interactor)
        {
            base.OnInteract(interactor);

            _interacted = false;

            if (_combat.IsAggressive())
            {
                interactor.GetComponent<Combat>().SetTarget(this);
            }
            else
            {
                print("Initiate dialogue");

                if (_speaker != null)
                {
                    _speaker.Initiate();
                }
            }
        }
    }
}