using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Core.Dialogue;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    [SerializeField] private string characterName;

    [SerializeField] private Dialogue[] dialogues;
    
    private Queue<Dialogue> _dialogues = new Queue<Dialogue>();

    private UIManager _uiManager;
    
    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        
        ResetDialogue();
    }

    private bool NextDialogue()
    {
        if (_dialogues.Count > 0)
        {
            Dialogue dialogue = _dialogues.Peek();
            _uiManager.ShowDialogue(dialogue, this);
            _dialogues.Dequeue();
            
            return true;
        }

        return false;
    }

    private void FirstDialogue()
    {
        Dialogue dialogue = _dialogues.Peek();
        FindObjectOfType<UIManager>().ShowDialogue(dialogue,this);
        _dialogues.Dequeue();
    }
    
    public void ShowDialogue()
    {
        if (!NextDialogue())
        {
            _uiManager.HideDialogue(this);
        }
    }

    public void ResetDialogue()
    {
        foreach (var dialogue in dialogues)
        {
            _dialogues.Enqueue(dialogue);
        }
    }

    private void DialogueState()
    {
        FirstDialogue();
    }
    
    public void Initiate()
    {
        print("interachyt");
        
        DialogueState();
    }
}
