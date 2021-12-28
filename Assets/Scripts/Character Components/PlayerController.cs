using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Character
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _mainCamera;
        
        private Movement _movement;
        private Combat _combat;
        private Interactor _interactor;
        private Health _health;

        private UIManager _uiManager;
        
        private bool _busy = false;

        public Health PlayerHealth => _health;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
            _movement = GetComponent<Movement>();
            _combat = GetComponent<Combat>();
            _interactor = GetComponent<Interactor>();
            _health = GetComponent<Health>();
            
            _uiManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            _health.UpdateHealth();
        }

        // Update is called once per frame
        void Update()
        {
            if(!_health.IsAlive()) return;

            //_movement.SetInjured(_health.GetHealth() <= _health.GetMaxHealth() * 0.1f);

            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _uiManager.TogglePauseMenu();
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
            
            if(_busy) return;

            if (Input.GetKeyDown(KeyCode.T))
            {
                _health.TakeDamage(10);
            }
            
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
                Interactable interactable = hit.transform.GetComponent<Interactable>();
                if (interactable != null)
                {
                    _interactor.Interact(interactable);
                    print("Interactable");
                }
                else
                {
                    if(_combat.HasTarget()) _combat.RemoveTarget();
                    
                    Move(hit.point);
                }
            }
        }

        private void Attack(int weapon)
        {
            _combat.Attack(weapon);
        }

        private void Move(Vector3 position)
        {
            _movement.Move(position);
        }

        public Combat GetCombat()
        {
            return _combat;
        }

        public void SetBusy(bool busy)
        {
            _busy = busy;
            _uiManager.ToggleHealthbar(busy);
        }
    }
}

