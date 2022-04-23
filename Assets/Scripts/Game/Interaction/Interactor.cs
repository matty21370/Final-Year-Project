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

        private void Start()
        {
            _npcController = GetComponent<NpcController>();
        }

        public void Interact(Interactable interactable)
        {
            interactable.MoveToInteract(this);
        }

        public void OnFinished()
        {
            print("yeeeeeee");
            if (_npcController != null)
            {
                _npcController.OnFinishedInteracting();
            }
        }

        public bool GetIsPlayer()
        {
            return isPlayer;
        }
    }
}