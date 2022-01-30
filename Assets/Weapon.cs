using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public string name;
    public float damage;
    public float range;
}
