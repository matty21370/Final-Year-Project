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
        [SerializeField] private GameObject restrictedNotification;
        [SerializeField] private GameObject healthbar;
        [SerializeField] private GameObject characterMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject deathScreen;

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
            deathScreen.SetActive(true);
        }
    
        public void UpdateHealthbar(float health, float maxHealth)
        {
            healthbar.GetComponent<Slider>().maxValue = maxHealth;
            healthbar.GetComponent<Slider>().value = health;
        }
    
        public void ToggleHealthbar(bool busy)
        {
            healthbar.SetActive(!busy);
        }
    
        public void ToggleCharacterMenu()
        {
            _characterMenuOpen = !_characterMenuOpen;
            characterMenu.SetActive(_characterMenuOpen);
            _player.SetBusy(_characterMenuOpen ? true : false);
        }

        public void TogglePauseMenu()
        {
            _pauseMenuOpen = !_pauseMenuOpen;
            pauseMenu.SetActive(_pauseMenuOpen);
            Time.timeScale = _pauseMenuOpen ? 0 : 1;
        }
    }
}


