using System;
using System.Collections;
using System.Collections.Generic;
using Game.Dialogue;
using Game.UI;
using Game.Interaction;
using Game.Inventory;
using Game.Items.Weapons;
using Game.Questing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Game.Character
{
    [Serializable]
    public class CustomizationOptions
    {
        [SerializeField] private GameObject[] hair, beard, moustache;

        public GameObject[] Hair => hair;

        public GameObject[] Beard => beard;

        public GameObject[] Moustache => moustache;

        public void SetHair(int index)
        {
            if (index < 0 || index > hair.Length) return;
            
            foreach (var hGameObject in hair)
            {
                hGameObject.SetActive(false);
            }
            
            hair[index].SetActive(true);
        }
        
        public void SetBeard(int index)
        {
            if (index < 0 || index > beard.Length) return;
            
            foreach (var bGameObject in beard)
            {
                bGameObject.SetActive(false);
            }
            
            beard[index].SetActive(true);
        }
        
        public void SetMoustache(int index)
        {
            if (index < 0 || index > moustache.Length) return;
            
            foreach (var mGameObject in moustache)
            {
                mGameObject.SetActive(false);
            }
            
            moustache[index].SetActive(true);
        }
    }
    
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public static PlayerController Instance => _instance;
        
        private Camera _mainCamera;
        
        private Movement _movement;
        private Combat _combat;
        private Interactor _interactor;
        private Health _health;

        private bool _busy = false;

        [SerializeField] private GameObject slotParent;

        [SerializeField] private EquipmentSlot[] equipmentSlots;
        [SerializeField] private WeaponSlot[] weaponSlots;

        [SerializeField] private CustomizationOptions customizationOptions;

        public CustomizationOptions CustomizationOptions => customizationOptions;

        private Weapon _unarmed;
        
        public Health PlayerHealth => _health;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            _mainCamera = Camera.main;
            _movement = GetComponent<Movement>();
            _combat = GetComponent<Combat>();
            _interactor = GetComponent<Interactor>();
            _health = GetComponent<Health>();
        }

        public void InitWeapons(Weapon weapon1, Weapon weapon2)
        {
            weaponSlots[0].SetWeapon(weapon1);
            weaponSlots[1].SetWeapon(weapon2);
        }

        private void InitWeapons()
        {
            _unarmed = ItemDatabase.Instance.GetItem("Unarmed") as Weapon; 
            
            weaponSlots[0].SetWeapon(_unarmed);
            weaponSlots[1].SetWeapon(_unarmed);
        }
        
        private void Start()
        {
            _health.UpdateHealth();
            
            Invoke(nameof(InitWeapons), 0.5f);
        }

        // Update is called once per frame
        void Update()
        {
            if(!_health.IsAlive()) return;

            HandleInput();
        }

        public void ApplyCustomization(CharacterAppearance characterAppearance)
        {
            customizationOptions.SetHair(characterAppearance.Hair);
            customizationOptions.SetBeard(characterAppearance.Beard);
            customizationOptions.SetMoustache(characterAppearance.Moustache);
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UIManager.Instance.ToggleCharacterMenu();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                _health.TakeDamage(10f);
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.Instance.TogglePauseMenu();
            }
            
            if(_busy) return;

            if (Input.GetKeyDown(KeyCode.T))
            {
                InventorySystem.Instance.AddItem(ItemDatabase.Instance.GetItem());
            }
            
            if (Input.GetMouseButton(0))
            {
                HandleClick();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                equipmentSlots[0].UseItem();
            } 
            else if (Input.GetKeyDown(KeyCode.A))
            {
                equipmentSlots[1].UseItem();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                equipmentSlots[2].UseItem();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                equipmentSlots[3].UseItem();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                WaypointPath.Instance.SetDestination(FindObjectOfType<NpcController>().transform);
            }
            
            if (_combat.IsInCombat())
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (weaponSlots[0].WeaponInSlot != null)
                    {
                        _combat.Attack(weaponSlots[0].WeaponInSlot);
                    }
                    else
                    {
                        _combat.Attack(_unarmed);
                    }
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (weaponSlots[1].WeaponInSlot != null)
                    {
                        _combat.Attack(weaponSlots[1].WeaponInSlot);
                    }
                    else
                    {
                        _combat.Attack(_unarmed);
                    }
                }
            }
        }

        private Interactable _interactable;
        
        private void HandleClick()
        {
            if(_busy) return;
            
            if(EventSystem.current.IsPointerOverGameObject()) return;
            
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && !hit.transform.CompareTag("unclickabble"))
            {
                Interactable interactable = hit.transform.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (interactable.Conditional)
                    {
                        Quest quest = QuestManager.Instance.ActiveQuest;
                        if (quest.GetCurrentObjective().Identifier == interactable.Condition)
                        {
                            _interactor.Interact(interactable);
                            _interactable = interactable;
                        }
                    }
                    else
                    {
                        _interactor.Interact(interactable);
                        _interactable = interactable;
                    }
                }
                else
                {
                    if(_combat.HasTarget()) _combat.RemoveTarget();
                    
                    if (_interactable != null)
                    {
                        _interactable.CancelInteraction();
                        _interactable = null;
                    }
                    
                    _interactor.SetInteracting(false);
                    Move(hit.point);
                }
            }
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
            UIManager.Instance.ToggleHealthbar(busy);
        }

        public void HideEquipmentSlots(bool hide)
        {
            slotParent.GetComponent<CanvasGroup>().alpha = hide ? 0 : 1;
        }
    }
}

