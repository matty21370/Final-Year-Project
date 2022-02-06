using System;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        private static DialogueSystem _instance;
        public static DialogueSystem Instance => _instance;

        private Speaker _activeSpeaker;

        [SerializeField] private Text nameText, speechText;

        [SerializeField] private GameObject dialogueUI;

        private DialogueSequence _activeSequence;

        private GameObject _buttonContainer;
        [SerializeField] private GameObject buttonPrefab;

        private bool _yeet = false;

        [SerializeField] private CanvasGroup endDialogueButton;

        public GameObject nextDialogueButton;
        
        private List<GameObject> _activeButtons = new List<GameObject>();
        private List<DialogueSequence> _sequences = new List<DialogueSequence>();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            //_buttonContainer = GameObject.Find("Options");
        }

        public void Initialization(GameObject buttonContainer)
        {
            _buttonContainer = buttonContainer;
        }

        public void Initiate(Speaker speaker, DialogueSequence sequence)
        {
            _activeSpeaker = speaker;
            _activeSequence = sequence;
        }

        public void Activate(Speaker speaker, DialogueSequence[] sequences)
        {
            if(_yeet) return;

            _activeSpeaker = speaker;
            nameText.text = _activeSpeaker.CharacterName;
            nextDialogueButton.SetActive(false);

            foreach (var sequence in sequences)
            {
                _sequences.Add(sequence);
            }
            
            SetUpButtons();

            endDialogueButton.alpha = 1;
            
            dialogueUI.SetActive(true);
            SetDialogue(speaker.OpeningDialogue);
            FindObjectOfType<PlayerController>().SetBusy(true);
            _yeet = true;
        }

        public void Reactivate()
        {
            SetUpButtons();
        }

        public void InitiateSequence(DialogueSequence sequence)
        {
            DestroyButtons();

            nextDialogueButton.SetActive(true);
            endDialogueButton.alpha = 0;
            _activeSequence = sequence;
            SetDialogue(_activeSequence.GetNextDialogue());
        }
        
        public void NextDialogue()
        {
            if(_activeSpeaker == null && _activeSequence == null) return;
            
            DialogueAction dialogue = _activeSequence.GetNextDialogue();
            if(dialogue != null) 
                SetDialogue(dialogue);
            else 
                EndOfSequence();
        }
        
        public void EndOfSequence()
        {
            speechText.text = _activeSpeaker.EndingDialogue;
            nextDialogueButton.SetActive(false);
            _activeSequence.Reset();
            SetUpButtons();
            endDialogueButton.alpha = 1;
        }

        public void EndDialogue()
        {
            _activeSpeaker = null;
            _activeSequence = null;
            _sequences.Clear();
            _yeet = false;
            DestroyButtons();
            FindObjectOfType<PlayerController>().SetBusy(false);
            SetDialogue("");
            dialogueUI.SetActive(false);
        }
        
        private void SetUpButtons()
        {
            foreach (var sequence in _sequences)
            {
                DialogueButton newButton = Instantiate(buttonPrefab, _buttonContainer.transform).GetComponent<DialogueButton>();
                newButton.Init(_activeSpeaker, sequence);
                _activeButtons.Add(newButton.gameObject);
            }
        }

        private void DestroyButtons()
        {
            foreach (var button in _activeButtons)
            {
                Destroy(button);
            }
        }

        private void SetDialogue(DialogueAction dialogue)
        {
            speechText.text = dialogue.dialogue;
            if (dialogue.e != null)
            {
                dialogue.e.Invoke();
            }
        }
        
        private void SetDialogue(string text)
        {
            speechText.text = text;
        }

    }
}