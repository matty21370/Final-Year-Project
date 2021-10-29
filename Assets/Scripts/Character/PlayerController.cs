using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Character
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _mainCamera;
        
        private Movement _movement;
        private Combat _combat;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _movement = GetComponent<Movement>();
            _combat = GetComponent<Combat>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButton(0))
            {
                 HandleClick();
            }
        }

        private void HandleClick()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Interactable interact = hit.transform.GetComponent<Interactable>();
                if (interact != null)
                {
                    interact.OnInteract(this);
                }
                else
                {
                    if(_combat.HasTarget()) _combat.RemoveTarget();
                    MoveToMouse(hit.point);
                }
            }
        }

        private void MoveToMouse(Vector3 position)
        {
            _movement.Move(position);
        }

        public Combat GetCombat()
        {
            return _combat;
        }
    }
}

