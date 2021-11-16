using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyBase;

    private GameManager _manager;
    
    // Start is called before the first frame update
    void Start()
    {
        _manager = FindObjectOfType<GameManager>();

        EntityData data = new EntityData();
        data.entityName = "Enemy";
        data.maxHealth = 50;
        data.movementSpeed = 5.4f;

        StartCoroutine(Load(data));
    }

    private IEnumerator Load(EntityData data)
    {
        yield return new WaitForSeconds(1f);
        
        LoadEnemy(data);
    }

    private void LoadEnemy(EntityData data)
    {
        GameObject newEnemy = Instantiate(enemyBase, new Vector3(1000, 1000, 1000), Quaternion.identity);
        NPCController controller = newEnemy.GetComponent<NPCController>();

        controller.ApplyData(data);
        
        _manager.AddEnemy(newEnemy);
        OnFinished();
    }

    private void OnFinished()
    {
        _manager.ConstructLeveledLists();
    }
}
