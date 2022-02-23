using System;
using Game.Saving;
using UnityEngine;
using Game.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject characterMenu;
        private SavingSystem _savingSystem;

        private void Awake()
        {
            QualitySettings.vSyncCount = 1;
            _savingSystem = GetComponent<SavingSystem>();
        }

        public void Save()
        {
            characterMenu.SetActive(true);
            _savingSystem.Save("save");
            characterMenu.SetActive(false);
        }

        public void Load()
        {
            characterMenu.SetActive(true);
            _savingSystem.Load("save");
            FindObjectOfType<UIManager>().TogglePauseMenu();
            characterMenu.SetActive(false);
        }
    }
}