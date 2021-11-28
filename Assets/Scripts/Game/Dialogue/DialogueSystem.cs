using System;
using Game.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private Text nameText, speechText;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetDialogue(string yeet, string text)
        {
            nameText.text = yeet;
            speechText.text = text;
        }

        public void ShowDialogue(Speaker speaker, string dialogue)
        {
            _canvasGroup.alpha = 1;
            SetDialogue(speaker.CharacterName, dialogue);
            FindObjectOfType<PlayerController>().SetBusy(true);
        }

        public void HideDialogue(Speaker speaker)
        {
            _canvasGroup.alpha = 0;
            SetDialogue("", "");
            FindObjectOfType<PlayerController>().SetBusy(false);
            speaker.ResetDialogue();
        }
    }
}