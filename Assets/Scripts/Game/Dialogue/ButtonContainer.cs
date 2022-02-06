using System;
using UnityEngine;

namespace Game.Dialogue
{
    public class ButtonContainer : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueSystem;
        
        private void Start()
        {
            DialogueSystem.Instance.Initialization(gameObject);
            dialogueSystem.SetActive(false);
        }
    }
}