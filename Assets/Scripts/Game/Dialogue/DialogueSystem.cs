using System;
using System.Collections.Generic;
using Game.Character;
using Game.Interaction;
using Game.Questing;
using Game.UI;
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
        [SerializeField] private DialogueUI ui;
        [SerializeField] private CanvasGroup endDialogueButton;
        
        private Speaker _activeSpeaker;
        private DialogueSequence _activeSequence;

        private PlayerController _playerController;

        private bool _active = false;
        
        private readonly List<GameObject> _activeButtons = new List<GameObject>();
        private readonly List<DialogueSequence> _sequences = new List<DialogueSequence>();

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

            _playerController = FindObjectOfType<PlayerController>();
        }

        private void Start()
        {
            EndDialogue();
        }

        public void Activate(Speaker speaker, DialogueSequence[] sequences)
        {
            if(_active) return;

            ui.OnOpen();
            
            _activeSpeaker = speaker;
            nameText.text = _activeSpeaker.CharacterName;
            nextDialogueButton.SetActive(false);

            foreach (var sequence in sequences)
            {
                if (sequence.conditional)
                {
                    if (QuestManager.Instance.ActiveQuest != null && QuestManager.Instance.ActiveQuest.GetCurrentObjective().Identifier == sequence.condition)
                    {
                        _sequences.Add(sequence);
                    }
                }
                else
                {
                    _sequences.Add(sequence);
                }
            }
            
            SetUpButtons();

            endDialogueButton.alpha = 1;
            
            dialogueUI.SetActive(true);
            SetDialogue(speaker.OpeningDialogue);
            _playerController.SetBusy(true);
            _playerController.HideEquipmentSlots(true);
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
            Quest activeQuest = QuestManager.Instance.ActiveQuest;
            if (activeQuest != null)
            {
                if (activeQuest.GetCurrentObjective().Goal == Objective.Goals.Talk)
                {
                    if (activeQuest.GetCurrentObjective().Identifier == _activeSequence.condition)
                    {
                        activeQuest.GetCurrentObjective().CompleteTarget(_activeSpeaker.GetComponent<Interactable>());
                    }
                }
            }
            LeanTween.move(dialogueUI.GetComponent<RectTransform>(), new Vector3(-75, 110, 0), 0.1f);
            speechText.text = _activeSpeaker.EndingDialogue;
            nextDialogueButton.SetActive(false);
            _activeSequence.Reset();
            SetUpButtons();
            endDialogueButton.alpha = 1;
        }

        public void EndDialogue()
        {
            ui.OnClose();
            _activeSpeaker = null;
            _activeSequence = null;
            _sequences.Clear();
            _active = false;
            DestroyButtons();
            _playerController.SetBusy(false);
            _playerController.HideEquipmentSlots(false);
            SetDialogue("");
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