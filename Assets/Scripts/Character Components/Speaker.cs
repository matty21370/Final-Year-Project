using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Dialogue;
using UnityEngine;
using UnityEngine.Events;

public class Speaker : MonoBehaviour
{
    [SerializeField] private string characterName;

    [SerializeField] private DialogueAction[] dialogues;
    [SerializeField] private bool repeating;
    private bool _spoken;
    
    private Queue<DialogueAction> _dialogues = new Queue<DialogueAction>();

    private UIManager _uiManager;

    public string CharacterName => characterName;
    
    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        
        ResetDialogue();
    }

    public void ResetDialogue()
    {
        foreach (var dialogue in dialogues)
        {
            _dialogues.Enqueue(dialogue);
        }
    }
    
    public void Initiate()
    {
        if (!_spoken)
        {
            ShowDialogue();
        }
    }
    
    public void ShowDialogue()
    {
        if (!NextDialogue())
        {
            _uiManager.HideDialogue(this);
            _spoken = !repeating;
        }
    }
    
    private bool NextDialogue()
    {
        if (_dialogues.Count > 0)
        {
            DialogueAction dialogueAction = _dialogues.Dequeue();
            UnityEvent dialogueEvent = dialogueAction.e;

            if (dialogueEvent != null)
            {
                dialogueEvent.Invoke();
            }

            _uiManager.ShowDialogue(dialogueAction.dialogue, this);
            
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class DialogueAction
{
    public string dialogue;
    public UnityEvent e;
}
