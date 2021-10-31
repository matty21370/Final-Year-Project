using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

public class Sandbox : MonoBehaviour
{
    [SerializeField] private Interactable[] interactables;

    public Interactable GetRandomInteractable()
    {
        int rand = Random.Range(0, interactables.Length);
        return interactables[rand];
    }
}
