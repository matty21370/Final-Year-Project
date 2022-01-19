using System;
using System.Collections;
using System.Collections.Generic;
using Game.Interaction.Interactables;
using Game.Items.Weapons;
using Game.Saving;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Character
{
    [RequireComponent(typeof(Target))]
    public class Combat : MonoBehaviour, ISaveable
    {
        private Target _target;

        private Animator _animator;
        private Movement _movement;

        [SerializeField] private bool isPlayer = false;
        
        [SerializeField] private float combatRange = 1.5f;
        [SerializeField] private float attackCooldown = 1f;

        [SerializeField] private Transform weaponHand;

        private float _nextAttack;

        [SerializeField] private string[] weapons;
        private Weapon[] _equippedWeapons = new Weapon[4];
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

        private void Start()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                _equippedWeapons[i] = (Weapon)FindObjectOfType<ItemDatabase>().GetItem(weapons[i]);
            }
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
                
                switch (_equippedWeapons[weapon].WeaponType)
                {
                    case Weapon.WeaponTypes.Unarmed:
                        int variant = Random.Range(1, 3);
                        _animator.SetTrigger("unarmed" + variant); 
                        print("unarmed" + variant);
                        break;
                    case Weapon.WeaponTypes.Sword:
                        _animator.SetTrigger("sword");
                        break;
                }

                _nextAttack = Time.time + attackCooldown;
            }
        }

        public void EquipWeapon(int index, Weapon weapon)
        {
            if (index >= _equippedWeapons.Length)
            {
                Debug.LogWarning("Equipping weapon at invalid index!");
                return;
            }

            _equippedWeapons[index] = weapon;
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
            if (_target != null)
            {
                _target.GetComponent<Health>().TakeDamage(_equippedWeapons[_currentWeapon].Damage);
            }
        }

        public bool IsAggressive()
        {
            return _isAggressive;
        }

        public void SetAggressive(bool val)
        {
            _isAggressive = val;
        }

        public object CaptureState()
        {
            Dictionary<string, object> saveData = new Dictionary<string, object>();
            saveData["inCombat"] = _inCombat;
            saveData["aggressive"] = _isAggressive;

            return saveData;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>) state;
            _inCombat = (bool) data["inCombat"];
            _isAggressive = (bool) data["aggressive"];
        }
    }
}


