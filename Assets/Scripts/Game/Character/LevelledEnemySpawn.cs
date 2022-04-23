using System;
using UnityEngine;

namespace Game.Character
{
    public class LevelledEnemySpawn : MonoBehaviour
    {
        [SerializeField] private LevelledEnemy levelledEnemy;

        private bool _enemyHasDied = false;
        
        private void Start()
        {
            SpawnEnemy();
        }

        public void CheckSpawn()
        {
            if (_enemyHasDied)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            Instantiate(levelledEnemy, transform.position, Quaternion.identity);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }
}