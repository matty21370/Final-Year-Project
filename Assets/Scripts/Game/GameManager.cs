using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();
    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private List<int> _levels = new List<int>();
    private Dictionary<List<GameObject>, int> _leveledLists = new Dictionary<List<GameObject>, int>();

    public void AddEnemy(GameObject enemyToAdd)
    {
        _enemies.Add(enemyToAdd);
        NPCController controller = enemyToAdd.GetComponent<NPCController>();
        int level = controller.GetLevel();
        if (!_levels.Contains(level))
        {
            _levels.Add(level);
        }
    }

    public void AddSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoints.Add(spawnPoint);
    }

    public List<GameObject> GetEnemies()
    {
        return _enemies;
    }

    public GameObject GetRandomEnemy()
    {
        return _enemies[Random.Range(0, _enemies.Count)];
    }

    public void SpawnEnemies()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            spawnPoint.SetObject(GetRandomEnemy());
            spawnPoint.Spawn();
        }
    }
    
    public void ConstructLeveledLists()
    {
        foreach (var level in _levels)
        {
            List<GameObject> tmp = new List<GameObject>();
            
            foreach (var enemy in _enemies)
            {
                if (enemy.GetComponent<NPCController>().GetLevel() == level)
                {
                    tmp.Add(enemy);
                }
            }
            
            _leveledLists.Add(tmp, level);
        }
        
        print("Constructed " + _leveledLists.Count + " levelled lists.");
        SpawnEnemies();
    }
}
