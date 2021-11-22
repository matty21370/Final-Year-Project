using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Character.AI;
using UnityEngine;
using UnityEngine.XR;

public class NPCController : MonoBehaviour
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
    [SerializeField] private float patrolDelay = 1f;
    [SerializeField] private float patrolSpeed = 2.3f;
    [SerializeField] private float timeAtWaypoint = 2f;

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
    }

    // Update is called once per frame
    void Update()
    {  
        if(!_health.IsAlive()) return;
        
        if (Vector3.Distance(_player.position, transform.position) < detectionRadius)
        {
            _combat.SetTarget(_player.GetComponent<Target>());
        }
        
        HandleBehaviour();
    }

    private void HandleBehaviour()
    {
        if (startingBehaviour == BehaviourTypes.Patrol)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        _movement.SetSpeed(patrolSpeed);
        _movement.Move(patrolPath.GetCurrentWaypoint(_patrolIndex));

        if (AtWaypoint())
        {
            _timeSpentAtWaypoint += Time.deltaTime;

            if (_timeSpentAtWaypoint >= timeAtWaypoint)
            {
                _timeSpentAtWaypoint = 0;
                _patrolIndex = patrolPath.GetNextWaypoint(_patrolIndex);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    public int GetLevel()
    {
        return _level;
    }

    private bool AtWaypoint()
    {
        bool reached = Vector3.Distance(transform.position, patrolPath.GetCurrentWaypoint(_patrolIndex)) <= 0.2;
        return reached;
    }
}
