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
        
        private Interactor _interacting;
        
        protected bool Interacted = false;

        private void Update()
        {
            if (_interacting != null && !Interacted)
            {
                if (Vector3.Distance(_interacting.transform.position, transform.position) <= interactionRange)
                {
                    _interacting.transform.LookAt(transform);
                    _interacting.GetComponent<Movement>().Stop();

                    StartCoroutine(WaitForInteract());
                }
            }
        }

        public void MoveToInteract(Interactor interactor)
        {
            if(GetComponent<Interactor>() != null) if(GetComponent<Interactor>().GetIsPlayer() && interactor.GetIsPlayer()) return;
            
            print("yeet");
            _interacting = interactor;
            _interacting.GetComponent<Movement>().Move(transform.position);
        }

        private IEnumerator WaitForInteract()
        {
            yield return new WaitForSeconds(interactionTime);

            print(_interacting);
            OnInteract(_interacting);
        }

        public virtual void OnInteract(Interactor interactor)
        {
            _interacting.OnFinished();
            _interacting = null;
            Interacted = true;
            
            if (QuestManager.Instance.ActiveQuest != null)
            {
                Objective objective = QuestManager.Instance.ActiveQuest.GetCurrentObjective();
                if (objective.Goal == Objective.Goals.Interact)
                {
                    foreach (Interactable interactable in objective.Targets)
                    {
                        if (interactable == this)
                        {
                            //StartCoroutine(Delay(objective));
                            objective.CompleteTarget(this);
                        }
                    }
                }
            }
            
            print(interactor);
        }
        
        private IEnumerator Delay(Objective objective)
        {
            yield return new WaitForSeconds(1f);
            objective.CompleteTarget(this);
        }

        public void HandleMarker(bool enable)
        {
            if(marker == null) return;
            
            marker.SetActive(enable);
        }
    }
}

