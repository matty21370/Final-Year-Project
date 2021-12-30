using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Dialogue;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup restrictedNotification;
        [SerializeField] private CanvasGroup healthbar;
        [SerializeField] private CanvasGroup characterMenu;
        [SerializeField] private CanvasGroup pauseMenu;
        [SerializeField] private CanvasGroup deathScreen;

        [SerializeField] private GameObject dialogueUI;
        private Speaker _speaker;
        private DialogueSystem _dialogueSystem;
    
        private bool _characterMenuOpen = false;
        private bool _pauseMenuOpen = false;
    
        private PlayerController _player;
    
        private void Awake()
        {
            _dialogueSystem = FindObjectOfType<DialogueSystem>();
            _player = FindObjectOfType<PlayerController>();
        }
    
        public void ShowDialogue(string dialogue, Speaker speaker)
        {
            _dialogueSystem.ShowDialogue(speaker, dialogue);
            _speaker = speaker;
        }
    
        public void HideDialogue(Speaker speaker)
        {
            _dialogueSystem.HideDialogue(speaker);
        }
    
        public void NextDialogue()
        {
            if(_speaker == null) return;
            
            _speaker.ShowDialogue();
        }
    
        public void ShowDeathScreen()
        {
            deathScreen.alpha = 1;
        }
    
        public void UpdateHealthbar(float health, float maxHealth)
        {
            healthbar.GetComponent<Slider>().maxValue = maxHealth;
            healthbar.GetComponent<Slider>().value = health;
        }
    
        public void ToggleHealthbar(bool busy)
        {
            healthbar.alpha = busy ? 0 : 1;
        }
    
        public void ToggleCharacterMenu()
        {
            _characterMenuOpen = !_characterMenuOpen;
            characterMenu.alpha = _characterMenuOpen ? 1 : 0;
            _player.SetBusy(_characterMenuOpen ? true : false);
        }

        public void TogglePauseMenu()
        {
            _pauseMenuOpen = !_pauseMenuOpen;
            pauseMenu.alpha = _pauseMenuOpen ? 1 : 0;
            Time.timeScale = _pauseMenuOpen ? 0 : 1;
        }
    }
}


