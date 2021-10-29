using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Core;
using UnityEngine;

public class Combat : MonoBehaviour, Interactable
{
    private Combat _target;

    private Movement _movement;

    [SerializeField] private float combatRange = 1.5f;
    
    [SerializeField] private Weapon[] equippedWeapons;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            HandleTarget();
        }
    }

    private void HandleTarget()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) <= combatRange)
        {
            _movement.Stop();
            if (Input.GetKeyDown(KeyCode.W))
            {
                Attack(equippedWeapons[0]);
            } 
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Attack(equippedWeapons[1]);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Attack(equippedWeapons[2]);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Attack(equippedWeapons[3]);
            }
        }
        else
        {
            _movement.Move(_target.transform.position);
        }
    }

    private void Attack(Weapon weapon)
    {
        print("Attacking with damage: " + weapon.damage);
    }

    public void SetTarget(Combat newTarget)
    {
        _target = newTarget;
    }

    public bool HasTarget()
    {
        return _target != null;
    }

    public void RemoveTarget()
    {
        _target = null;
    }

    public void OnInteract(PlayerController player)
    {
        if(gameObject.CompareTag("Player")) return;
        
        player.GetCombat().SetTarget(this);
    }
}
