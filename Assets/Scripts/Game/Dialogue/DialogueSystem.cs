using System;
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

        [SerializeField] private GameObject dialogueUI;

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
        }
        
        public void ShowDialogue(Speaker speaker, string dialogue)
        {
            dialogueUI.SetActive(true);
            SetDialogue(speaker.CharacterName, dialogue);
            FindObjectOfType<PlayerController>().SetBusy(true);
        }
        
        private void SetDialogue(string yeet, string text)
        {
            nameText.text = yeet;
            speechText.text = text;
        }

        public void HideDialogue(Speaker speaker)
        {
            dialogueUI.SetActive(false);
            SetDialogue("", "");
            FindObjectOfType<PlayerController>().SetBusy(false);
            speaker.ResetDialogue();
        }
    }
}