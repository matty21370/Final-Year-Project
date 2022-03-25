using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;

public class Alerter : MonoBehaviour
{
    [SerializeField] private Combat[] alert;

    public void Alert()
    {
        foreach (var character in alert)
        {
            character.SetAggressive(true);
        }
    }
}
