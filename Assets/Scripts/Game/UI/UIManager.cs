using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Dialogue;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.UI
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

        [SerializeField] private GameObject xpBar;
        private Slider _xpSlider;
        private CanvasGroup _xpCanvasGroup;
        private Text _xpBarText;
    
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
            _xpSlider = xpBar.GetComponentInChildren<Slider>();
            _xpCanvasGroup = xpBar.GetComponent<CanvasGroup>();
            _xpBarText = xpBar.GetComponentInChildren<Text>();
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

        public void CloseCharacterMenu()
        {
            characterMenu.SetActive(false);
        }

        public void TogglePauseMenu()
        {
            _pauseMenuOpen = !_pauseMenuOpen;
            pauseMenu.SetActive(_pauseMenuOpen);
            Time.timeScale = _pauseMenuOpen ? 0 : 1;
        }

        private bool _isRunning = false;
        
        public void UpdateXpBar(float xp)
        {
            _xpSlider.maxValue = LevellingSystem.Instance.XpToLevel;
            _xpBarText.text = "+" + xp + " XP";

            if (!_isRunning)
            {
                StartCoroutine(XpBarSequence());
            }
        }
        
        private IEnumerator XpBarSequence()
        {
            StartCoroutine(FadeIn(_xpCanvasGroup));
            _isRunning = true;

            yield return new WaitForSeconds(1.5f);

            StartCoroutine(FadeOut(_xpBarText.GetComponent<CanvasGroup>()));
            
            yield return new WaitForSeconds(2f);

            StartCoroutine(FadeOut(_xpCanvasGroup));
            
            yield return new WaitForSeconds(1f);
            _xpBarText.GetComponent<CanvasGroup>().alpha = 1f;
            _isRunning = false;
        }

        private IEnumerator FadeIn(CanvasGroup canvasGroup)
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += 0.01f;
                yield return new WaitForSeconds(0.001f);
            }
        }
        
        private IEnumerator FadeOut(CanvasGroup canvasGroup)
        {
            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= 0.01f;
                yield return new WaitForSeconds(0.001f);
            }
        }

        private void Update()
        {
            if (Math.Abs(_xpSlider.value - LevellingSystem.Instance.Xp) > 0.01f)
            {
                _xpSlider.value = Mathf.Lerp(_xpSlider.value, LevellingSystem.Instance.Xp, Time.deltaTime * 3f);
            }
        }

        public void UpdateXpBarLevelUp()
        {
            Slider slider = xpBar.GetComponentInChildren<Slider>();

            slider.maxValue = LevellingSystem.Instance.XpToLevel;
            slider.value = Mathf.MoveTowards(slider.value, LevellingSystem.Instance.Xp, Time.deltaTime * 6);
        }

        private IEnumerator LevellingSequence()
        {
            yield return new WaitForSeconds(1f);
            
            
        }
    }
}


