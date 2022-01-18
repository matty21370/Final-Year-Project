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

        private UIManager _uiManager;

        private void Awake()
        {
            _uiManager = FindObjectOfType<UIManager>();
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
                Kill(1);    
            }
        }
        
        //Use only for loading!
        private void SetHealth(float amt)
        {
            _currentHealth = amt;
            
            if (_currentHealth == 0)
            {
                Kill(1);    
            }
            else
            {
                GetComponent<Animator>().SetTrigger("revive");
                _isAlive = true;
            }
            
            UpdateHealth();
        }

        public void Heal(float amt)
        {
            _currentHealth = Mathf.Min(maxHealth, _currentHealth + amt);
            UpdateHealth();
        }

        public void Kill(int deathVar)
        {
            HandleDeath(deathVar);
        }

        private void HandleDeath(int deathVar)
        {
            _isAlive = false;
            GetComponent<Animator>().SetTrigger("die" + deathVar);
            GetComponent<Combat>().RemoveTarget();

            if (gameObject.CompareTag("Player"))
            {
                StartCoroutine(PlayerDeath());
            }
        }

        private IEnumerator PlayerDeath()
        {
            yield return new WaitForSeconds(2f);
            
            FindObjectOfType<UIManager>().ShowDeathScreen();
        }

        public void UpdateHealth()
        {
            if(!gameObject.CompareTag("Player")) return;
            
            _uiManager.UpdateHealthbar(_currentHealth, maxHealth);
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
            //saveData["isAlive"] = _isAlive;
            return saveData;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> saveData = (Dictionary<string, object>) state;
            float h = (float) saveData["currentHealth"];
            if (h <= 0)
            {
                HandleDeath(1);
            }
            else 
            {
                SetHealth((float) saveData["currentHealth"]);
            }
        }
    }
}

