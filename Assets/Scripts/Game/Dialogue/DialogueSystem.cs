using System;
using Game.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private Text nameText, speechText;

        [SerializeField] private GameObject dialogueUI;

        public void SetDialogue(string yeet, string text)
        {
            nameText.text = yeet;
            speechText.text = text;
        }

        public void ShowDialogue(Speaker speaker, string dialogue)
        {
            dialogueUI.SetActive(true);
            SetDialogue(speaker.CharacterName, dialogue);
            FindObjectOfType<PlayerController>().SetBusy(true);
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