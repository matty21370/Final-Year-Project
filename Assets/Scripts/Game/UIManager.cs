using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Dialogue;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup restrictedNotification;

    [SerializeField] private GameObject dialogueUI;
    private Speaker _speaker;

    private DialogueSystem _dialogueSystem;

    [SerializeField] private GameObject deathScreen;

    private void Awake()
    {
        _dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    public void ShowDialogue(Dialogue dialogue, Speaker speaker)
    {
        _dialogueSystem.ShowDialogue(speaker, dialogue);
        _speaker = speaker;
    }

    public void HideDialogue(Speaker speaker)
    {
        _dialogueSystem.HideDialogue(speaker);
    }

    public void NextDialogue()
    {
        if(_speaker == null) return;
        
        _speaker.ShowDialogue();
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
