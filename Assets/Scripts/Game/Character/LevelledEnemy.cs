using System;
using UnityEngine;

namespace Game.Character
{
    public class LevelledEnemy : MonoBehaviour
    {
        [SerializeField] private float level;

        private Health _health;
        private Combat _combat;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _combat = GetComponent<Combat>();
        }

        private void Init(float lvl)
        {
            level = lvl;
            float newHealth = _health.GetMaxHealth() + ((_health.GetMaxHealth() * level) / 10);
            _health.SetMaxHealth(newHealth);
        }
    }
}