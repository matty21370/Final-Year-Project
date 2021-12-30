using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MenuSystem : MonoBehaviour
    {
        [SerializeField] private GameObject startingScreen;
        
        private GameObject _currentScreen;

        private void Awake()
        {
            ChangeScreen(startingScreen);
        }

        public void ChangeScreen(GameObject screen)
        {
            if(_currentScreen != null) _currentScreen.GetComponent<CanvasGroup>().alpha = 0;
            
            screen.GetComponent<CanvasGroup>().alpha = 1;
            _currentScreen = screen;
        }
    }
}