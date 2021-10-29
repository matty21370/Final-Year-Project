using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _mainCamera;
        
        private Movement _movement;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _movement = GetComponent<Movement>();
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
                
                MoveToMouse(hit.point);
            }
        }

        private void MoveToMouse(Vector3 position)
        {
            _movement.Move(position);
        }
    }
}

