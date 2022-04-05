using System;
using Game.Interaction.Interactables;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class NoticeBoardUI : MonoBehaviour
    {
        private static NoticeBoardUI _instance;

        public static NoticeBoardUI Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                gameObject.SetActive(true);
                _instance = this;
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [SerializeField] private NoticeBoardUIElement[] uiElements;

        public void OnNoticeBoardOpened(NoticeBoardItem[] items)
        {
            for (int i = 0; i < 6; i++)
            {
                uiElements[i].Init(items[i]);
            }
        }
    }
}