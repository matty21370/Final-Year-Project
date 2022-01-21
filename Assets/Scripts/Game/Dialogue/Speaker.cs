using System.Collections.Generic;
using Game.Saving;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Dialogue
{
    public class Speaker : MonoBehaviour, ISaveable
    {
        [SerializeField] private string characterName;
    
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
                UIManager.Instance.HideDialogue(this);
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
    
                UIManager.Instance.ShowDialogue(dialogueAction.dialogue, this);
                
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
}