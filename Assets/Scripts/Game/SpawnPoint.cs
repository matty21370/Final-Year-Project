using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject _objectToSpawn;
    [SerializeField] private int spawnLevel;

    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        
        _gameManager.AddSpawnPoint(this);
    }

    public void Spawn()
    {
        try
        {
            Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("WARNING: Spawn object not set!");
        }
    }

    public int GetSpawnLevel()
    {
        return spawnLevel;
    }
    
    public void SetObject(GameObject o)
    {
        _objectToSpawn = o;
    }
}
