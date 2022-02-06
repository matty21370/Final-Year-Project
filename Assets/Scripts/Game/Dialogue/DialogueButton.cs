using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogue
{
    public class DialogueButton : MonoBehaviour
    {
        private DialogueSequence _sequence;
        private Speaker _speaker;

        public void Init(Speaker speaker, DialogueSequence sequence)
        {
            _sequence = sequence;
            _speaker = speaker;
            GetComponentInChildren<Text>().text = sequence.promptText;
        }

        private void Update()
        {
            if (_sequence.spoken) GetComponent<Button>().interactable = false;
        }

        public void HandleClick()
        {
            if (_sequence is {spoken: false})
            {
                DialogueSystem.Instance.InitiateSequence(_sequence);
            }
        }
    }
}