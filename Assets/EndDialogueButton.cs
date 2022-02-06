using System;
using System.Collections;
using System.Collections.Generic;
using Game.Dialogue;
using UnityEngine;

public class EndDialogueButton : MonoBehaviour
{
    private void Awake()
    {
        DialogueSystem.Instance.nextDialogueButton = gameObject;
        gameObject.SetActive(false);
    }
}
