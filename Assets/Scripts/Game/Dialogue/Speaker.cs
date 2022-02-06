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
        [SerializeField] private string openingDialogue, endingDialogue;
    
        [SerializeField] private DialogueSequence[] sequences;
        private bool _spoken;
        
        public string CharacterName => characterName;

        public string OpeningDialogue => openingDialogue;
        public string EndingDialogue => endingDialogue;

        public void Initiate()
        {
            DialogueSystem.Instance.Activate(this, sequences);
        }

        public object CaptureState()
        {
            List<bool> list = new List<bool>();
            foreach (var sequence in sequences)
            {
                list.Add(sequence.spoken);
            }

            return list;
        }

        public void RestoreState(object state)
        {
            List<bool> list = (List<bool>) state;
            for (int i = 0; i < list.Count; i++)
            {
                sequences[i].spoken = list[i];
            }
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
        public string promptText;
        public DialogueAction[] dialogues;
        public bool repeating, spoken;
        
        private int _index = 0;

        public DialogueAction GetNextDialogue()
        {
            if (!repeating) spoken = true;
            _index++;
            try
            {
                return dialogues[_index];
            }
            catch (IndexOutOfRangeException e)
            {
                return null;
            }
        }

        public void Reset()
        {
            _index = 0;
        }

    }
}
