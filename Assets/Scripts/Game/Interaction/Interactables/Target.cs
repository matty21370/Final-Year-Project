using System;
using Game.Character;
using Game;
using Game.Dialogue;

namespace Game.Interaction.Interactables
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

            Interacted = false;

            if (_combat == null && _speaker != null)
            {
                _speaker.Initiate();
                return;
            }
            
            if (_combat.IsAggressive())
            {
                interactor.GetComponent<Combat>().SetTarget(this);
            }
            else
            {
                if (_speaker != null)
                {
                    _speaker.Initiate();
                }
            }
        }
    }
}