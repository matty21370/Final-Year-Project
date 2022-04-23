using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;

    private GameObject _spawnedObject;
    private NpcController _npcController;

    public GameObject SpawnedObject => _spawnedObject;
    
    public void Spawn()
    { 
        _spawnedObject = Instantiate(objectToSpawn);

        NpcController tmp = _spawnedObject.GetComponent<NpcController>();

        if (_npcController != null)
        {
            _npcController = tmp;
        }
    }

    public NpcController GetCharacterFromSpawn()
    {
        if (_npcController == null) return null;

        return _npcController;
    }
}
