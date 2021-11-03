using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false;

    public void Interact(Interactable interactable)
    {
        interactable.MoveToInteract(this);
    }

    public bool GetIsPlayer()
    {
        return isPlayer;
    }
}