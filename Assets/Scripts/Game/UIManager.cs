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
        private static UIManager _instance;
        public static UIManager Instance => _instance;
        
        [SerializeField] private GameObject restrictedNotification;
        [SerializeField] private GameObject healthbar;
        [SerializeField] private GameObject characterMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject deathScreen;

        [SerializeField] private GameObject dialogueUI;
    
        private bool _characterMenuOpen = false;
        private bool _pauseMenuOpen = false;
    
        private PlayerController _player;
    
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            
            _player = FindObjectOfType<PlayerController>();
        }
    
        public void ShowDeathScreen()
        {
            deathScreen.SetActive(true);
        }

        public void HideDeathScreen()
        {
            deathScreen.SetActive(false);
        }
    
        public void UpdateHealthbar(float health, float maxHealth)
        {
            healthbar.GetComponent<Slider>().maxValue = maxHealth;
            healthbar.GetComponent<Slider>().value = health;
        }
    
        public void ToggleHealthbar(bool busy)
        {
            healthbar.SetActive(!busy);
            
            if(_characterMenuOpen) healthbar.SetActive(true);
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


