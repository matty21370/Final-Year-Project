using System;
using System.Collections;
using System.Collections.Generic;
using Game.Saving;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Game.Character
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        private bool _isAlive = true;

        private Animator _animator;
        private static readonly int Die = Animator.StringToHash("die");
        private static readonly int Revive = Animator.StringToHash("revive");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            _currentHealth = Mathf.Max(_currentHealth - amount, 0);
            
            if (gameObject.CompareTag("Player"))
            {
                UpdateHealth();
            }
            
            if (_currentHealth == 0)
            {
                Kill();    
            }
        }
        
        //Use only for loading!
        private void SetHealth(float amt)
        {
            _currentHealth = amt;
            
            if (_currentHealth == 0)
            {
                Kill();    
            }
            else
            {
                _isAlive = true;
                _animator.SetTrigger(Revive);
            }
            
            UpdateHealth();
        }

        public void Heal(float amt)
        {
            _currentHealth = Mathf.Min(maxHealth, _currentHealth + amt);
            UpdateHealth();
        }

        public void Kill()
        {
            HandleDeath();
        }

        private void HandleDeath()
        {
            _isAlive = false;
            _animator.SetTrigger(Die);
            GetComponent<Combat>().RemoveTarget();

            if (gameObject.CompareTag("Player"))
            {
                StartCoroutine(PlayerDeath());
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

