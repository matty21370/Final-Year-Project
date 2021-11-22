using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static List<NPCData> _allData = new List<NPCData>();

    private void Start()
    {
        foreach (var data in _allData)
        {
            print(data.Name);
        }
    }
}
