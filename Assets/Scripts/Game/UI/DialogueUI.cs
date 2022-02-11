using System;
using UnityEngine;

namespace Game.UI
{
    public class DialogueUI : MonoBehaviour
    {
        private Vector3 _startingScale;

        private bool _closed = false;
        
        private void Awake()
        {
            _startingScale = transform.localPosition;

            if (!_closed)
            {
                OnClose();
            }
        }

        public void OnOpen()
        {
            LeanTween.scale(gameObject, Vector3.one, 0.05f);
        }

        public void OnClose()
        {
            _closed = true;
            LeanTween.scale(gameObject, Vector3.zero, 0.25f).setOnComplete(Deactivate);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}