using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;

public class NPCController : MonoBehaviour
{   
    private Combat _combat;
    private Health _health;
    
    [SerializeField] private float detectionRadius = 2f;

    private Transform _player;
    private Vector3 _startPosition;
    
    private void Awake()
    {
        _combat = GetComponent<Combat>();
        _health = GetComponent<Health>();
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
