using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;

namespace Game.Core
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private float interactionRange = 1.5f;
        [SerializeField] private float interactionTime = 1.0f;
        
        private Interactor _interacting;
        
        protected bool _interacted = false;

        private void Update()
        {
            if (_interacting != null && !_interacted)
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
            
            _interacting = interactor;
            _interacting.GetComponent<Movement>().Move(transform.position);
        }

        private IEnumerator WaitForInteract()
        {
            yield return new WaitForSeconds(interactionTime);
            
            try
            {
                OnInteract(_interacting);
            }
            catch (NullReferenceException e) { }
        }

        public virtual void OnInteract(Interactor interactor)
        {
            _interacting = null;
            _interacted = true;
        }
    }
}

