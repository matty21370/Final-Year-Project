using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UILetter : MonoBehaviour
    {
        private static UILetter _instance;

        public static UILetter Instance => _instance;

        [SerializeField] private GameObject background;
        [SerializeField] private Text content;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OpenLetter(string letterContent)
        {
            content.text = letterContent;
            background.SetActive(true);
        }

        public void CloseLetter()
        {
            background.SetActive(false);
            content.text = "";
        }
    }
}