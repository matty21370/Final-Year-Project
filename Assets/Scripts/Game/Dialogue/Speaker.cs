using System;
using System.Collections.Generic;
using System.Xml.Serialization;
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
        
        public string CharacterName => characterName;
        public string OpeningDialogue => openingDialogue;
        public string EndingDialogue => endingDialogue;

        private static List<Speaker> _allSpeakers = new List<Speaker>();

        public static Speaker GetSpeakerByName(string name)
        {
            foreach (var speaker in _allSpeakers)
            {
                if (speaker.characterName == name)
                {
                    return speaker;
                }
            }

            return null;
        }

        private void Awake()
        {
            _allSpeakers.Add(this);
        }

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
    [XmlRoot("Sequence")]
    public class DialogueSequence
    {
        public bool conditional = false;
        public string condition;
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
                Debug.LogWarning(e.Message);
                return null;
            }
        }

        public void Reset()
        {
            _index = 0;
        }

    }
}
