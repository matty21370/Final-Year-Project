using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();

    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    public void AddEnemy(GameObject enemyToAdd)
    {
        _enemies.Add(enemyToAdd);
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
}
