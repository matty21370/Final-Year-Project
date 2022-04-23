using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character.AI;
using Game.Interaction;
using Game.Interaction.Interactables;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Game.Character
{
    public class NpcController : MonoBehaviour
    {
        private int _level;
        
        private Combat _combat;
        private Health _health;
        private Movement _movement;
        
        [SerializeField] private float detectionRadius = 2f;

        private Transform _player;
        private Vector3 _startPosition;
        
        private enum BehaviourTypes
        {
            Stationary,
            Patrol,
            Sandbox
        }

        [SerializeField] private BehaviourTypes startingBehaviour;

        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float patrolSpeed = 2.3f;
        [SerializeField] private float timeAtWaypoint = 2f;

        [FormerlySerializedAs("susTime")] [SerializeField] private float suspicionTime = 4f;
        private float _suspicionTimer = 0;

        [SerializeField] private Interactable[] sandboxObjects;
        [SerializeField] private PatrolPath[] sandboxPaths;

        private Interactable _currentSandboxObject;
        private PatrolPath _currentSandboxPath;

        private PatrolPath _currentPath;
        
        private float _timeSpentAtWaypoint = 0;

        private int _patrolIndex = 0;
        
        private void Awake()
        {
            _combat = GetComponent<Combat>();
            _health = GetComponent<Health>();
            _movement = GetComponent<Movement>();
            _player = FindObjectOfType<PlayerController>().transform;
        }

        private void Start()
        {
            _startPosition = transform.position;
            
            GetComponent<Combat>().SetCollider(detectionRadius);
        }

        // Update is called once per frame
        void Update()
        {  
            if(!_health.IsAlive()) return;

            HandleBehaviour();
        }

        private void HandleBehaviour()
        {
            if (_combat.IsAggressive())
            {
                AwareBehaviour();
            }
            
            if (!_combat.HasTarget())
            {
                if (startingBehaviour == BehaviourTypes.Patrol)
                {
                    _currentPath = patrolPath;
                    Patrol();
                }
                else if (startingBehaviour == BehaviourTypes.Sandbox)
                {
                    SandboxBehaviour();
                }
            }
            else
            {
                AttackBehaviour();
            }
        }

        private void Patrol()
        {
            _movement.SetSpeed(patrolSpeed);
            _movement.Move(_currentPath.GetCurrentWaypoint(_patrolIndex));

            if (AtWaypoint())
            {
                _timeSpentAtWaypoint += Time.deltaTime;

                if (_timeSpentAtWaypoint >= timeAtWaypoint)
                {
                    _timeSpentAtWaypoint = 0;
                    _patrolIndex = _currentPath.GetNextWaypoint(_patrolIndex);
                    ReachedWaypoint();
                }
            }
        }

        private void AttackBehaviour()
        {
            _movement.SetSpeed(_movement.GetBaseSpeed());
        }

        private void AwareBehaviour()
        {
            if (Vector3.Distance(_player.position, transform.position) < detectionRadius)
            {
                _combat.SetTarget(_player.GetComponent<Target>());
                _suspicionTimer = 0;
            }
            else
            {
                _combat.RemoveTarget();
                _suspicionTimer += Time.deltaTime;
                if (_suspicionTimer >= suspicionTime)
                {
                    _movement.Move(_startPosition);
                }
            }
        }

        private void SandboxBehaviour()
        {
            if (_currentSandboxObject == null && _currentSandboxPath == null)
            {
                GetSandboxObject();
            }

            if (_currentSandboxObject != null)
            {
                GetComponent<Interactor>().Interact(_currentSandboxObject);
            }

            if (_currentSandboxPath != null)
            {
                _currentPath = _currentSandboxPath;
                Patrol();
            }
        }

        private void GetSandboxObject()
        {
            if (_currentSandboxObject != null || _currentSandboxPath != null) return; 
            Random random = new Random();
            int option = random.Next(0, 2); //0 = path, 1 = interact
            if (option == 0)
            {
                _currentSandboxPath = sandboxPaths[UnityEngine.Random.Range(0, sandboxPaths.Length)];
            }
            else
            {
                _currentSandboxObject = sandboxObjects[UnityEngine.Random.Range(0, sandboxObjects.Length)];
            }
        }

        private void ReachedWaypoint()
        {
            if (_currentSandboxPath != null)
            {
                ClearSandbox();
                GetSandboxObject();
            }
        }

        private void ClearSandbox()
        {
            _currentSandboxObject = null;
            _currentSandboxPath = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }

        //Called by interactor component
        public void OnFinishedInteracting()
        {
            ClearSandbox();
            GetSandboxObject();
        }
        
        public int GetLevel()
        {
            return _level;
        }

        private bool AtWaypoint()
        {
            bool reached = Vector3.Distance(transform.position, _currentPath.GetCurrentWaypoint(_patrolIndex)) <= 0.2;
            return reached;
        }

        public void Alert()
        {
            _combat.SetTarget(_player.GetComponent<Target>());
        }
    }
}


