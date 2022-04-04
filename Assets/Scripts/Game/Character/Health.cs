using System;
using System.Collections;
using System.Collections.Generic;
using Game.Interaction;
using Game.Inventory;
using Game.Questing;
using Game.Saving;
using UnityEngine;
using Game.UI;

namespace Game.Character
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        [SerializeField] private bool hideOnDeath;

        private bool _isAlive = true;

        private Animator _animator;
        private static readonly int Die = Animator.StringToHash("die");
        private static readonly int Revive = Animator.StringToHash("revive");

        [SerializeField] private float xpOnDeath;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            _currentHealth = Mathf.Max(_currentHealth - amount, 0);
            CheckHealth();
            
            if (gameObject.CompareTag("Player"))
            {
                UpdateHealth();
            }
            
            if (_currentHealth <= 0)
            {
                Kill();    
            }
        }
        
        //Use only for loading!
        private void SetHealth(float amt)
        {
            _currentHealth = amt;
            CheckHealth();
            UpdateHealth();
        }

        public void Heal(float amt)
        {
            _currentHealth = Mathf.Min(maxHealth, _currentHealth + amt);
            CheckHealth();
            UpdateHealth();
        }

        public void Kill()
        {
            HandleDeath();
        }

        private void CheckHealth()
        {
            if (_currentHealth <= 0)
            {
                Kill();    
            }
            else
            {
                _isAlive = true;
                if (_animator != null)
                {
                    _animator.SetTrigger(Revive);
                }
            }
        }

        private void HandleDeath()
        {
            if(GetComponent<Destructable>() != null) GetComponent<Destructable>().Destruct();

            if (hideOnDeath)
            {
                gameObject.SetActive(false);
            }
            
            _isAlive = false;
            if (GetComponent<Animator>() != null)
            {
                _animator.SetTrigger(Die);
            }

            GetComponent<Combat>().RemoveTarget();

            if (gameObject.CompareTag("Player"))
            {
                UIManager.Instance.CloseCharacterMenu();
                StartCoroutine(PlayerDeath());
            }
            else
            {
                LevellingSystem.Instance.GiveXp(xpOnDeath);
                
                if (QuestManager.Instance.ActiveQuest != null)
                {
                    Interactable myInteractable = GetComponent<Interactable>();
                    Objective objective = QuestManager.Instance.ActiveQuest.GetCurrentObjective();
                    if (objective.Goal == Objective.Goals.Kill)
                    {
                        foreach (Interactable interactable in objective.Targets)
                        {
                            if (interactable == myInteractable)
                            {
                                objective.CompleteTarget(myInteractable);
                            }
                        }
                    }
                }
            }
        }

        private IEnumerator PlayerDeath()
        {
            yield return new WaitForSeconds(2f);
            
            UIManager.Instance.ShowDeathScreen();
        }

        public void UpdateHealth()
        {
            if(!gameObject.CompareTag("Player")) return;
            
            UIManager.Instance.UpdateHealthbar(_currentHealth, maxHealth);
        }

        public float GetHealth()
        {
            return _currentHealth;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public bool IsAlive()
        {
            return _isAlive;
        }

        public void SetMaxHealth(float amt)
        {
            maxHealth = amt;
        }

        public object CaptureState()
        {
            Dictionary<string, object> saveData = new Dictionary<string, object>();
            saveData["currentHealth"] = _currentHealth;
            return saveData;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> saveData = (Dictionary<string, object>) state;
            float h = (float) saveData["currentHealth"];
            
            if (h <= 0)
            {
                HandleDeath();
            }
            else 
            {
                SetHealth((float) saveData["currentHealth"]);
            }
            
            _animator.ResetTrigger(Revive);
        }
    }
}

