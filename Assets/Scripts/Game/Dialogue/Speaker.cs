using System;
using System.Collections.Generic;
using Game.Saving;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Dialogue
{
    public class Speaker : MonoBehaviour, ISaveable
    {
        [SerializeField] private string characterName;
        [SerializeField] private string openingDialogue;
    
        [SerializeField] private DialogueAction[] dialogues;
        [SerializeField] private bool repeating;
        private bool _spoken;
        
        private Queue<DialogueAction> _dialogues = new Queue<DialogueAction>();
    
        public string CharacterName => characterName;
        
        private void Awake()
        {
            ResetDialogue();
        }
    
        public void ResetDialogue()
        {
            foreach (var sequence in dialogues)
            {
                _dialogues.Enqueue(sequence);
            }
        }
        
        public void Initiate()
        {
            if (!_spoken)
            {
                DialogueSystem.Instance.Initiate(this);
                ShowDialogue();
            }
        }
        
        public void ShowDialogue()
        {
            if (!NextDialogue())
            {
                DialogueSystem.Instance.HideDialogue(this);
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
    
                DialogueSystem.Instance.ShowDialogue(this, dialogueAction.dialogue);
                
                return true;
            }
            return false;
        }

        public object CaptureState()
        {
            return _spoken;
        }

        public void RestoreState(object state)
        {
            _spoken = (bool) state;
        }
    }
    
    [System.Serializable]
    public class DialogueAction
    {
        public string dialogue;
        public UnityEvent e;
    }

    [Serializable]
    public class DialogueSequence
    {
        public DialogueAction[] dialogues;
        public bool active;
    }
}
