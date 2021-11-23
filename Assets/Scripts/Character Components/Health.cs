using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Game.Character
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        private bool _isAlive = true;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            
            _currentHealth = Mathf.Max(_currentHealth - amount, 0);
            print(_currentHealth);

            if (_currentHealth == 0)
            {
                HandleDeath();    
            }
        }

        private void HandleDeath()
        {
            _isAlive = false;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Combat>().RemoveTarget();

            if (gameObject.tag == "Player")
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
    }
}

