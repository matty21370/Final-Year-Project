using System;
using System.Collections;
using System.Collections.Generic;
using Game.Interaction.Interactables;
using Game.Inventory;
using Game.Items.Weapons;
using Game.Saving;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Character
{
    [RequireComponent(typeof(Target))]
    public class Combat : MonoBehaviour, ISaveable
    {
        private Animator _animator;
        private Movement _movement;

        [SerializeField] private bool isPlayer = false;
        
        [SerializeField] private float combatRange = 1.5f;
        [SerializeField] private float attackCooldown = 1f;
        private Target _target;
        private float _nextAttack;
        
        [SerializeField] private Transform weaponHand;

        [SerializeField] private string[] weapons;
        private Items.Weapons.Weapon[] _equippedWeapons = new Items.Weapons.Weapon[2];
        //private int _currentWeapon;
        private Weapon _currentWeapon;

        private bool _inCombat = false;

        [SerializeField] private bool isAggressive = false;
        private bool _isAggressive;

        [SerializeField] private string factionName;
        private Faction _faction;

        [SerializeField] private string armour;
        
        //private static readonly int InCombat = Animator.StringToHash("inCombat");
        private static readonly int Sword = Animator.StringToHash("sword");

        private GameObject _weaponPrefab;
        private bool _hasModel = false;

        public string FactionName => factionName;

        private Clothing _clothing;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _movement = GetComponent<Movement>();
            _clothing = GetComponent<Clothing>();
            
            _isAggressive = isAggressive;
        }

        private void Start()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                _equippedWeapons[i] = (Items.Weapons.Weapon)FindObjectOfType<ItemDatabase>().GetItem(weapons[i]);
            }

            _faction = Faction.GetFactionByName(factionName);

            if (isPlayer)
            {
                GetComponent<PlayerController>().InitWeapons(_equippedWeapons[0], _equippedWeapons[1]);
            }

            if (armour.Length > 1)
            {
                _clothing.EquipArmourSet(armour);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_target != null)
            {
                HandleTarget();
            }

            if(GetComponent<Animator>() == null) return;
            
            if (_inCombat)
            {
                
                if (_equippedWeapons[0] == null)
                {
                    _animator.SetBool("inCombatUnarmed", _inCombat);
                    return;
                }
                
                if (_equippedWeapons[0].WeaponType == Items.Weapons.Weapon.WeaponTypes.Unarmed)
                {
                    _animator.SetBool("inCombatUnarmed", _inCombat);
                }
                else if (_equippedWeapons[0].WeaponType == Items.Weapons.Weapon.WeaponTypes.Sword)
                {
                    _animator.SetBool("inCombatSword", _inCombat);
                }
            }
            else
            {
                _animator.SetBool("inCombatSword", false);
                _animator.SetBool("inCombatUnarmed", false);
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
                    Attack(_equippedWeapons[0]);
                }
            }
            else
            {
                _movement.Move(_target.transform.position);
                _inCombat = false;
            } 
            
            if(!_target.GetComponent<Health>().IsAlive()) RemoveTarget();
        }

        public void Attack(Weapon weapon)
        {
            if (Time.time > _nextAttack)
            {
                _currentWeapon = weapon;
                
                switch (_currentWeapon.WeaponType)
                {
                    case Items.Weapons.Weapon.WeaponTypes.Unarmed:
                        if (_animator != null)
                        {
                            int variant = Random.Range(1, 3);
                            _animator.SetTrigger("unarmed" + variant);
                        }
                        else
                        {
                            Hit();
                        }

                        break;
                    case Items.Weapons.Weapon.WeaponTypes.Sword:
                        _animator.SetTrigger(Sword);
                        break;
                }

                _nextAttack = Time.time + attackCooldown;
            }
        }

        public void EquipWeapon(int index, Items.Weapons.Weapon weapon)
        {
            if (index >= _equippedWeapons.Length)
            {
                Debug.LogWarning("Equipping weapon at invalid index!");
                return;
            }

            _equippedWeapons[index] = weapon;
        }
        
        public void Hit()
        {
            if (_target != null)
            {
                _target.GetComponent<Health>().TakeDamage(_currentWeapon.Damage);
                print($"{gameObject.name} dealt {_currentWeapon.Damage} damage to {_target.gameObject.name}");
            }
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

        public void SetTarget(Target newTarget)
        {
            _target = newTarget;
            if (_equippedWeapons[0].HasModel() && !_hasModel)
            {
                _weaponPrefab = Instantiate(_equippedWeapons[0].GetModel(), weaponHand);
                _hasModel = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(isPlayer) return;
            if(HasTarget()) return;
            
            Combat collided = other.GetComponent<Combat>();

            if(collided == null) return;

            if (collided.IsEnemyOfFaction(_faction))
            {
                SetTarget(collided.GetComponent<Target>());
            }
        }

        public bool HasTarget()
        {
            return _target != null;
        }

        public void RemoveTarget()
        {
            _target = null;
            _inCombat = false;
            if (_weaponPrefab != null)
            {
                Destroy(_weaponPrefab);
                _hasModel = false;
            }
        }

        public bool IsInCombat()
        {
            return _inCombat;
        }

        public float GetAttackCooldown()
        {
            return attackCooldown;
        }

        public bool IsAggressive()
        {
            return _isAggressive;
        }

        public void SetAggressive(bool val)
        {
            _isAggressive = val;
        }

        public bool IsEnemyOfFaction(Faction f)
        {
            return _faction.IsEnemy(f);
        }

        public void SetCollider(float radius)
        {
            //GetComponent<SphereCollider>().radius = radius;
        }

    }

    public class Faction
    {
        public static List<Faction> AllFactions = new List<Faction>();

        public static Faction GetFactionByName(string name)
        {
            foreach (var faction in AllFactions)
            {
                if (faction.FactionName == name)
                {
                    return faction;
                }
            }

            return null;
        }

        public static void RegisterFaction(string name)
        {
            Faction faction = new Faction(name);
            AllFactions.Add(faction);
        }

        private string _factionName;

        private List<Faction> _enemyFactions = new List<Faction>();

        public string FactionName => _factionName;
        
        private Faction(string name)
        {
            _factionName = name;
        }

        public void AssignEnemy(Faction faction)
        {
            _enemyFactions.Add(faction);
        }

        public bool IsEnemy(Faction faction)
        {
            foreach (var f in _enemyFactions)
            {
                if (f.FactionName == faction.FactionName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}


