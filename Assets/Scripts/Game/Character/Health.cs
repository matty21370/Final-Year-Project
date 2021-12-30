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

        public void TakeDamage(float amount, bool heal)
        {
            if (heal)
            {
                _currentHealth = amount;
            }
            else
            {
                _currentHealth = Mathf.Max(_currentHealth - amount, 0);
            }
            
            if (gameObject.CompareTag("Player"))
            {
                UpdateHealth();
            }
            
            print("Healthh");
            
            if (_currentHealth == 0)
            {
                Kill(1);    
            }
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
            FindObjectOfType<UIManager>().ShowDeathScreen();

            yield return new WaitForSeconds(4f);

            SceneManager.LoadScene(0);
        }

        public void UpdateHealth()
        {
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
            return _currentHealth;
        }

        public void RestoreState(object state)
        {
            TakeDamage((float) state, true);
        }
    }
}

