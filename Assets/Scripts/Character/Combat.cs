using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Core;
using UnityEngine;

public class Combat : MonoBehaviour, Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract(PlayerController player)
    {
        if(gameObject.CompareTag("Player")) return;
        
        //player.GetCombat(

        print("Interacting with combat");
    }
}
