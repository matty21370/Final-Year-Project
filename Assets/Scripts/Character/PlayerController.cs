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

            if (_combat.IsInCombat())
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Attack(0);
                
                } 
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    Attack(1);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    Attack(2);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    Attack(3);
                }
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

        private void Attack(int weapon)
        {
            _combat.Attack(weapon);
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

