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
        
        [SerializeField] private Text nameText, speechText;
        [SerializeField] private GameObject dialogueUI, buttonPrefab, buttonContainer, nextDialogueButton;
        [SerializeField] private CanvasGroup endDialogueButton;
        
        private Speaker _activeSpeaker;
        private DialogueSequence _activeSequence;

        private bool _active = false;
        
        private List<GameObject> _activeButtons = new List<GameObject>();
        private List<DialogueSequence> _sequences = new List<DialogueSequence>();

        private Rect _startingPosition, _speakingPosition;

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

            _startingPosition = new Rect(-75, 110, 689, 161);
            _speakingPosition = new Rect(0, 110, 689, 161);
        }
        
        public void Activate(Speaker speaker, DialogueSequence[] sequences)
        {
            if(_active) return;

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
            _active = true;
        }

        public void InitiateSequence(DialogueSequence sequence)
        {
            DestroyButtons();
            dialogueUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 110);

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
            dialogueUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 110);
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
            _active = false;
            DestroyButtons();
            FindObjectOfType<PlayerController>().SetBusy(false);
            SetDialogue("");
            dialogueUI.SetActive(false);
        }
        
        private void SetUpButtons()
        {
            foreach (var sequence in _sequences)
            {
                DialogueButton newButton = Instantiate(buttonPrefab, buttonContainer.transform).GetComponent<DialogueButton>();
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