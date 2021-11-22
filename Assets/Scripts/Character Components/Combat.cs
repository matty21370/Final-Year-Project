using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Core;
using UnityEngine;

[RequireComponent(typeof(Target))]
public class Combat : MonoBehaviour
{
    private Target _target;

    private Animator _animator;
    private Movement _movement;

    [SerializeField] private bool isPlayer = false;
    
    [SerializeField] private float combatRange = 1.5f;
    [SerializeField] private float attackCooldown = 1f;

    private float _nextAttack;
    
    [SerializeField] private Weapon[] equippedWeapons;
    private int _currentWeapon;

    private bool _inCombat = false;

    [SerializeField] private bool isAggressive = false;
    private bool _isAggressive;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();

        _isAggressive = isAggressive;
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
            transform.LookAt(_target.transform);
            _inCombat = true;
            _movement.Stop();

            if (!isPlayer)
            {
                Attack(0);
            }
        }
        else
        {
            _movement.Move(_target.transform.position);
            _inCombat = false;
        }
        
        if(!_target.GetComponent<Health>().IsAlive()) RemoveTarget();
    }

    public void Attack(int weapon)
    {
        if (Time.time > _nextAttack)
        {
            _currentWeapon = weapon;
            _animator.SetTrigger("attack");
            _nextAttack = Time.time + attackCooldown;
        }
    }

    public void SetTarget(Target newTarget)
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

    public float GetAttackCooldown()
    {
        return attackCooldown;
    }

    public void Hit()
    {
        _target.GetComponent<Health>().TakeDamage(equippedWeapons[_currentWeapon].damage);
    }

    public bool IsAggressive()
    {
        return _isAggressive;
    }

    public void SetAggressive(bool val)
    {
        _isAggressive = val;
    }
    
}
