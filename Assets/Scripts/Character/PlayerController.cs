using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class PlayerController : MonoBehaviour
    {
        private Movement _movement;

        private void Awake()
        {
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
                MoveToMouse();    
            }
        }

        private void MoveToMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _movement.Move(hit.point);
            }
        }
    }
}

