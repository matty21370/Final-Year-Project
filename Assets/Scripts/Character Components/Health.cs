using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;

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
            
        }

        public float GetHealth()
        {
            return _currentHealth;
        }
    }
}

