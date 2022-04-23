using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;

namespace Game.Interaction
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private bool isPlayer = false;

        private NpcController _npcController;

        private bool _isInteracting = false;

        private void Start()
        {
            _npcController = GetComponent<NpcController>();
        }

        public void Interact(Interactable interactable)
        {
            if(_isInteracting) return;
            _isInteracting = true;
            interactable.MoveToInteract(gameObject.GetComponent<Interactor>());
        }

        public void OnFinished()
        {
            if (_npcController != null)
            {
                _npcController.OnFinishedInteracting();
            }

            _isInteracting = false;
            print("Setting interacting to false");
        }

        public bool GetIsPlayer()
        {
            return isPlayer;
        }
    }
}