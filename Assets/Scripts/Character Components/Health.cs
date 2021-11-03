using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            GetComponent<NavMeshAgent>().enabled = false;
        }

        public float GetHealth()
        {
            return _currentHealth;
        }

        public bool IsAlive()
        {
            return _isAlive;
        }
    }
}

