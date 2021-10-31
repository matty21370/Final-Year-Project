using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Core;
using UnityEngine;

public class Combat : MonoBehaviour, Interactable
{
    private Combat _target;

    private Animator _animator;
    private Movement _movement;

    [SerializeField] private float combatRange = 1.5f;
    
    [SerializeField] private Weapon[] equippedWeapons;

    private bool _inCombat = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            HandleTarget();
        }

        if (_inCombat)
        {
            _animator.SetBool("inCombat", true);
        }
        else
        {
            _animator.SetBool("inCombat", false);
        }
    }

    private void HandleTarget()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) <= combatRange)
        {
            _inCombat = true;
            _movement.Stop();
        }
        else
        {
            _movement.Move(_target.transform.position);
            _inCombat = false;
        }
    }

    public void Attack(int weapon)
    {
        _target.GetComponent<Health>().TakeDamage(equippedWeapons[weapon].damage);
        _animator.SetTrigger("attack");
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
        _inCombat = false;
    }

    public bool IsInCombat()
    {
        return _inCombat;
    }

    public void OnInteract(PlayerController player)
    {
        if(gameObject.CompareTag("Player")) return;
        
        player.GetCombat().SetTarget(this);
    }
}
