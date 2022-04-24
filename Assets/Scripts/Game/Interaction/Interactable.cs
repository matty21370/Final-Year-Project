using System;
using System.Collections;
using Game.Character;
using Game.Questing;
using UnityEngine;

namespace Game.Interaction
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private float interactionRange = 1.5f;
        [SerializeField] private float interactionTime = 1.0f;

        [SerializeField] private GameObject marker;

        [SerializeField] private bool reusable;
        
        private Interactor _interacting;
        
        protected bool Interacted = false;

        private bool _waitForInteract = false;

        private void Update()
        {
            if (_interacting != null && !Interacted)
            {
                if (Vector3.Distance(_interacting.transform.position, transform.position) <= interactionRange)
                {
                    _interacting.transform.LookAt(transform);
                    _interacting.GetComponent<Movement>().Stop();

                    if (!_waitForInteract)
                    {
                        StartCoroutine(WaitForInteract());
                        _waitForInteract = true;
                    }
                }
            }
        }

        public void MoveToInteract(Interactor interactor)
        {
            if (_interacting != null)
            {
                print("Already interacting!");
                return;
            }
            
            if (GetComponent<Interactor>() != null)
            {
                if (GetComponent<Interactor>().GetIsPlayer() && interactor.GetIsPlayer())
                {
                    return;
                }
            }
            
            _interacting = interactor;
            _interacting.GetComponent<Movement>().Move(transform.position);
        }

        public void CancelInteraction()
        {
            _interacting = null;
        }

        private IEnumerator WaitForInteract()
        {
            yield return new WaitForSeconds(interactionTime);
            
            OnInteract(_interacting);
        }

        public virtual void OnInteract(Interactor interactor)
        {
            
        }

        public void ResetInteractable(Interactor interactor)
        {
            interactor.OnFinished();
            
            _interacting = null;
            Interacted = !reusable;
            
            _waitForInteract = false;
            
            if (QuestManager.Instance.ActiveQuest != null)
            {
                Objective objective = QuestManager.Instance.ActiveQuest.GetCurrentObjective();
                if (objective.Goal == Objective.Goals.Interact)
                {
                    foreach (Interactable interactable in objective.Targets)
                    {
                        if (interactable == this)
                        {
                            objective.CompleteTarget(this);
                        }
                    }
                }
            }
        }

        public void HandleMarker(bool enable)
        {
            if(marker == null) return;
            
            marker.SetActive(enable);
        }
    }
}

